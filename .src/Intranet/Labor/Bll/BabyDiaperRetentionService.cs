#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the babydiaper retention service
    /// </summary>
    public class BabyDiaperRetentionService : ServiceBase, IBabyDiaperRetentionService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public IBabyDiaperRetentionBll BabyDiaperRetentionBll { get; set; }

        /// <summary>
        ///     Gets or sets the baby diaper retention service helper.
        /// </summary>
        public IBabyDiaperRetentionServiceHelper BabyDiaperRetentionServiceHelper { get; set; }

        /// <summary>
        ///     Gets or sets the baby diaper service helper.
        /// </summary>
        public IBabyDiaperServiceHelper BabyDiaperServiceHelper { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyDiaperRetentionService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiaperRetentionService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        private static String CreateProductionCode( TestSheet testSheetInfo ) => "IT/" + testSheetInfo.MachineNr.Substring(1) + "/" + testSheetInfo.CreatedDateTime.Year.ToString().Substring(2) + "/";

        #region Implementation of IBabyDiaperRetentionService

        /// <summary>
        ///     Gets a new BabyDiaperRetentionEditViewModel
        /// </summary>
        /// <param name="retentionTestId">The Id of the Babydiaper retention test which will be edited</param>
        /// <returns>The BabyDiaperRetentionEditViewModel</returns>
        public BabyDiaperRetentionEditViewModel GetBabyDiapersRetentionEditViewModel( Int32 retentionTestId )
        {
            var testValue = BabyDiaperRetentionBll.GetTestValue( retentionTestId );
            if (testValue.IsNull())
            {
                Logger.Error("TestValue mit id " + retentionTestId + "existiert nicht in DB!");
                return null;
            }
            var babyDiapersTestValue = testValue.BabyDiaperTestValue;
            if (babyDiapersTestValue.IsNull())
            {
                Logger.Error("BabyDiaperRetentionTestValue mit id " + testValue.TestValueId + "existiert nicht in DB!");
                return null;
            }
            if ( babyDiapersTestValue.TestType != TestTypeBabyDiaper.Retention )
            {
                Logger.Error("Requestet test was not an Retention Test. Id " + testValue.TestValueId);
                return null;
            }
            var testSheetInfo = testValue.TestSheet;
            if (testSheetInfo.IsNull())
            {
                Logger.Error("TestBlatt mit id " + testValue.TestSheetRefId + "existiert nicht in DB!");
                return null;
            }
            var notes = testValue.TestValueNote;
            var errors = BabyDiaperRetentionBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value } )
                                   .ToList();
            if (notes.IsNull())
                notes = new List<TestValueNote>();
            var testNotes = notes.Select( note => new TestNote { Id = note.TestValueNoteId, ErrorCodeId = note.ErrorRefId, Message = note.Message } )
                                 .ToList();

            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestValueId = retentionTestId,
                TestSheetId = testValue.TestSheetRefId,
                TestPerson = testValue.LastEditedPerson,
                ProductionCode = BabyDiaperServiceHelper.CreateProductionCode( testSheetInfo ),
                ProductionCodeDay = testValue.DayInYearOfArticleCreation,
                ProductionCodeTime = babyDiapersTestValue.DiaperCreatedTime,
                DiaperWeight = babyDiapersTestValue.WeightDiaperDry,
                WeightRetentionWet = babyDiapersTestValue.RetentionWetWeight,
                Notes = testNotes,
                NoteCodes = errorCodes
            };
            return viewModel;
        }

        /// <summary>
        ///     Gets the BabyDiaperRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper retention test is for</param>
        /// <returns>The BabyDiaperRetentionEditViewModel</returns>
        public BabyDiaperRetentionEditViewModel GetNewBabyDiapersRetentionEditViewModel( Int32 testSheetId )
        {
            var testSheetInfo = BabyDiaperRetentionBll.GetTestSheetInfo( testSheetId );

            if ( testSheetInfo.IsNull() )
            {
                Logger.Error( "TestBlatt mit id " + testSheetId + "existiert nicht in DB!" );
                return null;
            }

            var errors = BabyDiaperRetentionBll.GetAllNoteCodes();
            var errorCodes = errors.Select(error => new ErrorCode { ErrorId = error.ErrorId, Name = error.ErrorCode + " - " + error.Value })
                                   .ToList();
            var viewModel = new BabyDiaperRetentionEditViewModel
            {
                TestSheetId = testSheetId,
                TestValueId = -1,
                ProductionCode = BabyDiaperServiceHelper.CreateProductionCode(testSheetInfo),
                NoteCodes = errorCodes,
                Notes = new List<TestNote>()
            };

            return viewModel;
        }

        /// <summary>
        ///     Saves or updates the BabyDiaperRetentionEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated BabyDiaperRetentionEditViewModel</returns>
        public TestValue Save( BabyDiaperRetentionEditViewModel viewModel )
        {
            TestValue testValue = null;
            try
            {
                testValue = viewModel.TestValueId <= 0
                    ? BabyDiaperRetentionServiceHelper.SaveNewRetentionTest(viewModel)
                    : BabyDiaperRetentionServiceHelper.UpdateRetentionTest(viewModel);
                var testSheet = BabyDiaperRetentionServiceHelper.UpdateRetentionAverageAndStv(viewModel.TestSheetId);
            }
            catch (Exception e)
            {
                Logger.Error("Update oder Create new Test Value ist fehlgeschlagen: " + e.Message);
            }
            return testValue;
        }

        /// <summary>
        ///     deletes the testvalue
        /// </summary>
        /// <param name="testValueId">id of the testvalue</param>
        /// <returns>The deleted testvalue</returns>
        public TestValue Delete( Int32 testValueId )
        {
            var result = BabyDiaperRetentionBll.DeleteTestValue( testValueId );
            BabyDiaperRetentionServiceHelper.UpdateRetentionAverageAndStv( result.TestSheetRefId );
            return result;
        }

        #endregion
    }
}