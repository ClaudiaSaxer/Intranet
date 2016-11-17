using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Labor.ViewModel;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing labor dashboard service
    /// </summary>
    public class LaborDashboardService : ServiceBase, ILaborDashboardService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the labor dashboard service
        /// </summary>
        /// <value>the bll</value>
        public ILaborDashboardBll LaborDashboardBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborDashboardService) ) )
        {
        }

        #endregion

        #region Implementation of ILaborDashboardService

        /// <summary>
        ///     Labor creator view model
        /// </summary>
        /// <returns>the LaborDashboardViewModel</returns>
        public LaborDashboardViewModel GetLaborDashboardViewModel()
        {
            var testsheets = LaborDashboardBll.GetTestSheetForActualAndLastThreeShifts();

            var items = new List<DashboardItem>();

            foreach ( var testSheet in testsheets )
            {
                var sheet = items.Find( item => item.MachineName.Equals( testSheet.MachineNr ) );
                if ( sheet == null )
                {
                    sheet = new DashboardItem
                    {
                        MachineName = testSheet.MachineNr,
                        ShiftItems =
                            new List<ShiftItem>
                            {
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() },
                                new ShiftItem { ProductionOrderItems = new List<ProductionOrderItem>() }
                            }
                    };
                    items.Add( sheet );
                }
                sheet.ShiftItems.ToList()[0].ProductionOrderItems.Add( new ProductionOrderItem
                                                                       {
                                                                           SheetId = testSheet.TestSheetId,
                                                                           HasNotes = testSheet.TestValues.ToList()
                                                                                               .Exists( value => value.TestValueNote.Count > 0 ),
                                                                           Notes = toDashboardNote( testSheet.TestValues )
                                                                           ,
                                                                           ProductionOrderName = testSheet.FaNr,
                                                                           RwType = ToRwTypeAll( testSheet.TestValues.ToList() ),
                                                                           Action = "Edit",
                                                                           Controller = testSheet.ArticleType == ArticleType.BabyDiaper ? "LaborCreatorBaby" : "LaborCreatorInko"
                                                                       } );
            }
            return new LaborDashboardViewModel { DashboardItem = items };

            #endregion
        }

        private RwType CalcRwType( RwType before, RwType? rwType )
        {
            if ( rwType == null )
                return before;
            if ( ( before == RwType.Worse ) || ( rwType == RwType.Worse ) )
                return RwType.Worse;
            return before == RwType.SomethingWorse ? RwType.SomethingWorse : rwType.Value;
        }

        private List<DashboardNote> toDashboardNote( IEnumerable<TestValue> testValues )
        {
            var notes = new List<DashboardNote>();

            foreach ( var testValue in testValues.Where( testValue => testValue.TestValueNote.Count > 0 ) )
                notes.AddRange(
                    testValue.TestValueNote.Select(
                                 testValueNote =>
                                         new DashboardNote { ErrorMessage = testValueNote.Error.Value, Message = testValueNote.Message, Code = testValueNote.Error.ErrorCode } ) );

            return notes;
        }

        private RwType ToRwTypeAll( IEnumerable<TestValue> testValues )
        {
            var rwType = RwType.Ok;
            foreach (
                var testValue in
                testValues.Where( testValue => ( testValue.TestValueType == TestValueType.Average ) || ( testValue.TestValueType == TestValueType.StandardDeviation ) ) )
                if ( testValue.ArticleTestType == ArticleType.BabyDiaper )
                {
                    rwType = CalcRwType( rwType, testValue.BabyDiaperTestValue.RetentionRw );
                    rwType = CalcRwType( rwType, testValue.BabyDiaperTestValue.PenetrationRwType );
                    rwType = CalcRwType( rwType, testValue.BabyDiaperTestValue.Rewet140Rw );
                    rwType = CalcRwType( rwType, testValue.BabyDiaperTestValue.Rewet210Rw );
                    rwType = CalcRwType( rwType, testValue.IncontinencePadTestValue.RetentionRw );
                }
                else
                {
                    rwType = CalcRwType( rwType, testValue.IncontinencePadTestValue.AcquisitionTimeFirstRw );
                    rwType = CalcRwType( rwType, testValue.IncontinencePadTestValue.AcquisitionTimeSecondRw );
                    rwType = CalcRwType( rwType, testValue.IncontinencePadTestValue.AcquisitionTimeThirdRw );
                    rwType = CalcRwType( rwType, testValue.IncontinencePadTestValue.RewetAfterAcquisitionTimeRw );
                    rwType = CalcRwType( rwType, testValue.IncontinencePadTestValue.RewetFreeRw );
                }

            return rwType;
        }
    }
}