using Intranet.Common;
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

        /// <summary>
        ///     Gets or sets the helper for the labor dashboard
        /// </summary>
        /// <value>the labor dashboard helper</value>
        public ILaborDashboardHelper LaborDashboardHelper { get; set; }

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
            var dashboardItemM10 = new DashboardItem { MachineName = "M10" };
            var dashboardItemM11 = new DashboardItem { MachineName = "M11" };
            var dashboardItemM49 = new DashboardItem { MachineName = "M49" };

            dashboardItemM10.ShiftItemsCurrent = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 0, "M10" ) );
            dashboardItemM10.ShiftItemsMinus1 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 1, "M10" ) );
            dashboardItemM10.ShiftItemsMinus2 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 2, "M10" ) );
            dashboardItemM10.ShiftItemsMinus3 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 3, "M10" ) );

            dashboardItemM11.ShiftItemsCurrent = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 0, "M11" ) );
            dashboardItemM11.ShiftItemsMinus1 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 1, "M11" ) );
            dashboardItemM11.ShiftItemsMinus2 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 2, "M11" ) );
            dashboardItemM11.ShiftItemsMinus3 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 3, "M11" ) );

            dashboardItemM49.ShiftItemsCurrent = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 0, "M49" ) );
            dashboardItemM49.ShiftItemsMinus1 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 1, "M49" ) );
            dashboardItemM49.ShiftItemsMinus2 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 2, "M49" ) );
            dashboardItemM49.ShiftItemsMinus3 = LaborDashboardHelper.ToProductionOrderItems( LaborDashboardBll.GetTestSheetForMinusXShiftPerMachineNr( 3, "M49" ) );

            return new LaborDashboardViewModel
            {
                DashboardItemM10 = dashboardItemM10,
                DashboardItemM11 = dashboardItemM11,
                DashboardItemM49 = dashboardItemM49
            };

            #endregion
        }
    }
}