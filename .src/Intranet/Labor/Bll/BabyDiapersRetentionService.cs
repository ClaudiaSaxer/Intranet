#region Usings

using System;
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
            var babyDiapersTestValue = BabyDiapersRetentionBll.GetBabyDiapersRetetionTest( retentionTestId );
            var testSheetInfo = BabyDiapersRetentionBll.GetTestSheetInfo( testValue.TestSheetRefId );
            var notes = BabyDiapersRetentionBll.GetNotes( testValue.TestValueId );
            var errors = BabyDiapersRetentionBll.GetAllNoteCodes();
            var errorCodes = errors.Select( error => error.ErrorCode + " - " + error.Value )
                                   .ToList();
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
        public BabyDiapersRetentionEditViewModel Save( BabyDiapersRetentionEditViewModel viewModel )
        {
            if ( viewModel.TestValueId <= 0 ) // New Test
            {
                viewModel = SaveNewTest( viewModel );
            }
            else  // update
            {
                viewModel = UpdateTest( viewModel );
            }
            return viewModel;
        }

        #endregion

        private BabyDiapersRetentionEditViewModel SaveNewTest(BabyDiapersRetentionEditViewModel viewModel)
        {
            var testValue = new TestValue
            {
                TestSheetRefId = viewModel.TestSheetId,
                CreatedDateTime = DateTime.Now,
                LastEditedDateTime = DateTime.Now,
                CreatedPerson = viewModel.TestPerson,
                LastEditedPerson = viewModel.TestPerson,
                DayInYearOfArticleCreation = viewModel.ProductionCodeDay,
                ArticleTestType = ArticleType.BabyDiaper
            };
            var babyDiaperTestValue = new BabyDiaperTestValue
            {
                DiaperCreatedTime = viewModel.ProductionCodeTime,
                WeightDiaperDry = viewModel.DiaperWeight,
                RetentionWetWeight = viewModel.WeightRetentionWet,
                TestType = TestTypeBabyDiaper.Retention
            };
            babyDiaperTestValue = CalculateBabyDiaperRetentionValues(babyDiaperTestValue, viewModel.TestSheetId );
            testValue.BabyDiaperTestValue = babyDiaperTestValue;

            BabyDiapersRetentionBll.SaveNewTestValue(testValue);
            return viewModel;
        }

        private BabyDiapersRetentionEditViewModel UpdateTest(BabyDiapersRetentionEditViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        private BabyDiaperTestValue CalculateBabyDiaperRetentionValues( BabyDiaperTestValue testValue, Int32 testSheetId )
        {
            var testSheet = BabyDiapersRetentionBll.GetTestSheetInfo( testSheetId );
            testValue.RetentionAfterZentrifugeValue = testValue.RetentionWetWeight - testValue.WeightDiaperDry;
            if ( Math.Abs( testValue.WeightDiaperDry ) > 0.1 )
                testValue.RetentionAfterZentrifugePercent = (testValue.RetentionWetWeight - testValue.WeightDiaperDry) * 100.0 / testValue.WeightDiaperDry;
            testValue.RetentionRw = RwType.Ok; // TODO auf db schauen wegen grenzwert
            testValue.SapType = testSheet.SAPType;
            testValue.SapNr = testSheet.SAPNr;
            testValue.SapGHoewiValue = 0.0; // TODO auf db schauen wegen grenzwert
            return testValue;
        }
    }
}