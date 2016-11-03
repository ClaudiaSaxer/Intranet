using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extend;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
{
    /// <summary>
    /// 
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
        public BabyDiapersRetentionService(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyDiapersRetentionService) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

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
            var testSheetInfo = BabyDiapersRetentionBll.GetTestSheetInfo(testValue.TestSheetRefId);
            var notesCode = 0;

            var viewModel = new BabyDiapersRetentionEditViewModel
            {
                Id = retentionTestId,
                TestSheetId = testValue.TestSheetRefId,
                TestPerson = testValue.LastEditedPerson,
                ProductionCode = CreateProductionCode(testSheetInfo),
                ProductionCodeTime = babyDiapersTestValue.DiaperCreatedTime,
                DiaperWeight = babyDiapersTestValue.WeightDiaperDry,
                WeightRetentionWet = 0 // TODO

            };

            /*var vm = new BabyDiapersRetentionEditViewModel { Id = 5, TestPerson = "Edit Hans", ProductionCode = "IT/11/16/158/" };
            vm.Notes = new List<TestNote>
            {
                new TestNote { Id=1,Message = "Hans was here."},
                new TestNote { Id=1,Message = "Franz was here, too. :P"}
            };*/
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
                Logger.Warn( "TestBlatt mit id " + testSheetId + "existiert nicht in DB!");
                return null;
            }

            var viewModel = new BabyDiapersRetentionEditViewModel();
            viewModel.ProductionCode = CreateProductionCode( testSheetInfo );

            return viewModel;
        }

        /// <summary>
        ///     Saves or updates the BabyDiapersRetentionEditViewModel
        /// </summary>
        /// <param name="babyDiapersRetentionEditViewModel">The viewmodel which will be saved or updated</param>
        /// <returns>The saved or updated BabyDiapersRetentionEditViewModel</returns>
        public BabyDiapersRetentionEditViewModel Save( BabyDiapersRetentionEditViewModel babyDiapersRetentionEditViewModel )
        {
            return babyDiapersRetentionEditViewModel;
        }

        #endregion

        private String CreateProductionCode( TestSheet testSheetInfo )
        {
            return "IT/" + testSheetInfo.MachineNr + "/" + testSheetInfo.CreatedDateTime.Year + "/" + testSheetInfo.DayInYear + "/";
        }
    }
}
