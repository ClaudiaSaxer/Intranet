﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the inko retention service
    /// </summary>
    public class InkoRetentionService : ServiceBase, IInkoRetentionService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the incontinence retention test.
        /// </summary>
        public ITestBll TestBll { get; set; }

        /// <summary>
        ///     Gets or sets the incontinence retention service helper.
        /// </summary>
        public IInkoRetentionServiceHelper InkoRetentionServiceHelper { get; set; }

        /// <summary>
        ///     Gets or sets the test service helper.
        /// </summary>
        public ITestServiceHelper TestServiceHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public InkoRetentionService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoRetentionService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IInkoRetentionService

        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        public TestValue Delete( Int32 testValueId )
        {
            var result = TestBll.DeleteTestValue( testValueId );
            InkoRetentionServiceHelper.UpdateRetentionAverageAndStv( result.TestSheetId);
            return result;
        }

        /// <summary>
        ///     Gets a new InkoRetentionEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Inko rewet test which will be edited</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRetentionEditViewModel GetInkoRetentionEditViewModel( Int32 rewetTestId )
        {
            var testValue = TestBll.GetTestValue( rewetTestId );
            if ( testValue.IsNull() )
            {
                Logger.Error( "TestValue mit id " + rewetTestId + "existiert nicht in DB!" );
                return null;
            }
            var incontinencePadTestValue = testValue.IncontinencePadTestValue;
            if ( incontinencePadTestValue.IsNull() )
            {
                Logger.Error( "IncontinencePadTestValue mit id " + testValue.TestValueId + "existiert nicht in DB!" );
                return null;
            }
            if ( incontinencePadTestValue.TestType != TestTypeIncontinencePad.Retention )
            {
                Logger.Error( "Requestet test was not an InkoRewet Test. Id " + testValue.TestValueId );
                return null;
            }
            var testSheet = testValue.TestSheet;
            if ( testSheet.IsNull() )
            {
                Logger.Error( "TestBlatt mit id " + testValue.TestSheetId + "existiert nicht in DB!" );
                return null;
            }
            var notes = testValue.TestValueNote;
            var errors = TestBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value } )
                                   .ToList();
            if ( notes.IsNull() )
                notes = new List<TestValueNote>();
            var testNotes = notes.Select( note => new TestNote { Id = note.TestValueNoteId, ErrorCodeId = note.ErrorId, Message = note.Message } )
                                 .ToList();

            var viewModel = new InkoRetentionEditViewModel
            {
                TestValueId = rewetTestId,
                TestSheetId = testValue.TestSheetId,
                TestPerson = testValue.LastEditedPerson,
                ProductionCode = TestServiceHelper.CreateProductionCode( testSheet ),
                ProductionCodeDay = testValue.DayInYearOfArticleCreation,
                ProductionCodeTime = incontinencePadTestValue.IncontinencePadTime,
                ExpireMonth = incontinencePadTestValue.ExpireMonth,
                ExpireYear = incontinencePadTestValue.ExpireYear,
                InkoWeight = incontinencePadTestValue.RetentionWeight,
                InkoWeightWet = incontinencePadTestValue.RetentionWetValue,
                InkoWeightAfterZentrifuge = incontinencePadTestValue.RetentionAfterZentrifuge,
                Notes = testNotes,
                NoteCodes = errorCodes
            };
            return viewModel;
        }

        /// <summary>
        ///     Gets the InkoRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Inko rewet test is for</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRetentionEditViewModel GetNewInkoRetentionEditViewModel( Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );

            if ( testSheet.IsNull() || ( testSheet.ArticleType != ArticleType.IncontinencePad ) )
            {
                Logger.Error( "TestBlatt mit id " + testSheetId + "existiert nicht in DB!" );
                return null;
            }

            var errors = TestBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value } )
                                   .ToList();

            var viewModel = new InkoRetentionEditViewModel
            {
                TestSheetId = testSheetId,
                TestValueId = -1,
                ProductionCode = TestServiceHelper.CreateProductionCode( testSheet ),
                NoteCodes = errorCodes,
                Notes = new List<TestNote>()
            };

            var oldTestValue = testSheet.TestValues.Where( t => t.TestValueType == TestValueType.Single )
                                        .ToList()
                                        .LastOrDefault();
            if ( oldTestValue == null )
                return viewModel;
            viewModel.TestPerson = oldTestValue.LastEditedPerson;
            viewModel.ProductionCodeDay = oldTestValue.DayInYearOfArticleCreation;
            viewModel.ProductionCodeTime = oldTestValue.IncontinencePadTestValue.IncontinencePadTime;
            viewModel.ExpireMonth = oldTestValue.IncontinencePadTestValue.ExpireMonth;
            viewModel.ExpireYear = oldTestValue.IncontinencePadTestValue.ExpireYear;

            return viewModel;
        }

        /// <summary>
        ///     Saves or updates the InkoRetentionEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        public TestValue Save( InkoRetentionEditViewModel viewModel )
        {
            TestValue testValue;
            try
            {
                testValue = viewModel.TestValueId <= 0
                    ? InkoRetentionServiceHelper.SaveNewRetentionTest( viewModel )
                    : InkoRetentionServiceHelper.UpdateRetentionTest( viewModel );
                InkoRetentionServiceHelper.UpdateRetentionAverageAndStv( viewModel.TestSheetId );
            }
            catch ( Exception e )
            {
                Logger.Error( "Update oder Create new Test Value ist fehlgeschlagen: " + e.Message );
                testValue = null;
            }
            return testValue;
        }

        #endregion
    }
}