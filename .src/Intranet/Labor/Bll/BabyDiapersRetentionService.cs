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
    /// </summary>
    public class BabyDiapersRetentionService : ServiceBase, IBabyDiapersRetentionService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the baby diapers retention test.
        /// </summary>
        public IBabyDiapersRetentionBll BabyDiapersRetentionBll { get; set; }

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
        public BabyDiapersRetentionService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(BabyDiapersRetentionService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        private String CreateProductionCode( TestSheet testSheetInfo )
        {
            return "IT/" + testSheetInfo.MachineNr + "/" + testSheetInfo.CreatedDateTime.Year + "/";
        }

        #region Implementation of IBabyDiapersRetentionService

        /// <summary>
        ///     Gets a new BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="retentionTestId">The Id of the Babydiaper retention test which will be edited</param>
        /// <returns>The BabyDiapersRetentionEditViewModel</returns>
        public BabyDiapersRetentionEditViewModel GetBabyDiapersRetentionEditViewModel( Int32 retentionTestId )
        {
            var testValue = BabyDiapersRetentionBll.GetTestValue( retentionTestId );
            var babyDiapersTestValue = testValue.BabyDiaperTestValue;
            var testSheetInfo = BabyDiapersRetentionBll.GetTestSheetInfo( testValue.TestSheetRefId );
            var notes = testValue.TestValueNote;
            var errors = BabyDiapersRetentionBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => error.ErrorCode + " - " + error.Value )
                                   .ToList();
            if (notes.IsNull())
                notes = new List<TestValueNote>();
            var testNotes = notes.Select( note => new TestNote { Id = note.TestValueNoteId, ErrorCodeId = note.ErrorRefId, Message = note.Message } )
                                 .ToList();

            var viewModel = new BabyDiapersRetentionEditViewModel
            {
                TestValueId = retentionTestId,
                TestSheetId = testValue.TestSheetRefId,
                TestPerson = testValue.LastEditedPerson,
                ProductionCode = CreateProductionCode( testSheetInfo ),
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
        ///     Gets the BabyDiapersRetentionEditViewModel for edit
        /// </summary>
        /// <param name="testSheetId">The Id of the test sheet where the Babydiaper retention test is for</param>
        /// <returns>The BabyDiapersRetentionEditViewModel</returns>
        public BabyDiapersRetentionEditViewModel GetNewBabyDiapersRetentionEditViewModel( Int32 testSheetId )
        {
            var testSheetInfo = BabyDiapersRetentionBll.GetTestSheetInfo( testSheetId );

            if ( testSheetInfo.IsNull() )
            {
                Logger.Warn( "TestBlatt mit id " + testSheetId + "existiert nicht in DB!" );
                return null;
            }

            var viewModel = new BabyDiapersRetentionEditViewModel();
            viewModel.TestSheetId = testSheetId;
            viewModel.TestValueId = -1;
            viewModel.ProductionCode = CreateProductionCode( testSheetInfo );

            return viewModel;
        }

        /// <summary>
        ///     Saves or updates the BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="viewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated BabyDiapersRetentionEditViewModel</returns>
        public TestValue Save( BabyDiapersRetentionEditViewModel viewModel )
        {
            TestValue testValue = null;
            try
            {
                testValue = viewModel.TestValueId <= 0
                    ? BabyDiaperServiceHelper.SaveNewRetentionTest(viewModel)
                    : BabyDiaperServiceHelper.UpdateRetentionTest(viewModel);
                var testSheet = BabyDiaperServiceHelper.UpdateAverageAndStv(viewModel.TestSheetId);
            }
            catch (Exception e)
            {
                Logger.Error("Update oder Create new Test Value ist fehlgeschlagen: " + e.Message);
            }
            return testValue;
        }

        #endregion
    }
}