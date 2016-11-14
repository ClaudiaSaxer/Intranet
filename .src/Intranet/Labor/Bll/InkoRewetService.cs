#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the inko rewet service
    /// </summary>
    public class InkoRewetService : ServiceBase, IInkoRewetService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the incontinence rewet test.
        /// </summary>
        public ITestBll TestBll { get; set; }

        /// <summary>
        ///     Gets or sets the incontinence rewet service helper.
        /// </summary>
        public IInkoRewetServiceHelper InkoRewetServiceHelper { get; set; }

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
        public InkoRewetService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(InkoRewetService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IInkoRewetService

        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        public TestValue Delete( Int32 testValueId )
        {
            var result = TestBll.DeleteTestValue( testValueId );
            InkoRewetServiceHelper.UpdateRewetAverageAndStv(result.TestSheetRefId);
            return result;
        }

        /// <summary>
        ///     Gets a new InkoRewetEditViewModel
        /// </summary>
        /// <param name="rewetTestId">The Id of the Inko rewet test which will be edited</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRewetEditViewModel GetInkoRewetEditViewModel( Int32 rewetTestId )
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
            if ( incontinencePadTestValue.TestType != TestTypeIncontinencePad.RewetFree )
            {
                Logger.Error( "Requestet test was not an InkoRewet Test. Id " + testValue.TestValueId );
                return null;
            }
            var testSheet = testValue.TestSheet;
            if ( testSheet.IsNull() )
            {
                Logger.Error( "TestBlatt mit id " + testValue.TestSheetRefId + "existiert nicht in DB!" );
                return null;
            }
            var notes = testValue.TestValueNote;
            var errors = TestBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value } )
                                   .ToList();
            if ( notes.IsNull() )
                notes = new List<TestValueNote>();
            var testNotes = notes.Select( note => new TestNote { Id = note.TestValueNoteId, ErrorCodeId = note.ErrorRefId, Message = note.Message } )
                                 .ToList();

            var viewModel = new InkoRewetEditViewModel
            {
                TestValueId = rewetTestId,
                TestSheetId = testValue.TestSheetRefId,
                TestPerson = testValue.LastEditedPerson,
                ProductionCode = TestServiceHelper.CreateProductionCode( testSheet ),
                ProductionCodeDay = testValue.DayInYearOfArticleCreation,
                ProductionCodeTime = incontinencePadTestValue.IncontinencePadTime,
                FPDry = incontinencePadTestValue.RewetFreeDryValue,
                FPWet = incontinencePadTestValue.RewetFreeWetValue,
                Notes = testNotes,
                NoteCodes = errorCodes
            };
            return viewModel;
        }

        /// <summary>
        ///     Gets the InkoRewetEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Inko rewet test is for</param>
        /// <returns>The InkoRewetEditViewModel</returns>
        public InkoRewetEditViewModel GetNewInkoRewetEditViewModel( Int32 testSheetId )
        {
            var testSheet = TestBll.GetTestSheetInfo( testSheetId );

            if ( testSheet.IsNull() )
            {
                Logger.Error( "TestBlatt mit id " + testSheetId + "existiert nicht in DB!" );
                return null;
            }

            var errors = TestBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value } )
                                   .ToList();

            var viewModel = new InkoRewetEditViewModel
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
            if ( oldTestValue != null )
            {
                viewModel.TestPerson = oldTestValue.LastEditedPerson;
                viewModel.ProductionCodeDay = oldTestValue.DayInYearOfArticleCreation;
                viewModel.ProductionCodeTime = oldTestValue.IncontinencePadTestValue.IncontinencePadTime;
            }

            return viewModel;
        }

        /// <summary>
        ///     Saves or updates the InkoRewetEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated TestValue</returns>
        public TestValue Save( InkoRewetEditViewModel viewModel )
        {
            TestValue testValue = null;
            try
            {
                testValue = viewModel.TestValueId <= 0
                    ? InkoRewetServiceHelper.SaveNewRewetTest(viewModel)
                    : InkoRewetServiceHelper.UpdateRewetTest(viewModel);
                var testSheet = InkoRewetServiceHelper.UpdateRewetAverageAndStv(viewModel.TestSheetId);
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