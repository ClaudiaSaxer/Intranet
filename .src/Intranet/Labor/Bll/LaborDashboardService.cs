using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;

namespace Intranet.Labor.Bll
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

        /// <summary>
        ///     Gets or sets the shift helper
        /// </summary>
        /// <value>the shift helper</value>
        public IShiftHelper ShiftHelper { get; set; }

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

            var shifts = ShiftHelper.GetLastXShiftSchedule( 4 );
            var items = LaborDashboardBll.GetTestSheetForShifts( shifts );

            var dictionary = items.GroupBy( sheet => sheet.MachineNr )
                                  .ToDictionary( sheets => sheets.Key, sheets => sheets.ToList() );

            dashboardItemM10.ShiftItemsCurrent = DictionaryToProductionOrderItem( "M10", shifts[0], dictionary );
            dashboardItemM10.ShiftItemsMinus1 = DictionaryToProductionOrderItem( "M10", shifts[1], dictionary );
            dashboardItemM10.ShiftItemsMinus2 = DictionaryToProductionOrderItem( "M10", shifts[2], dictionary );
            dashboardItemM10.ShiftItemsMinus3 = DictionaryToProductionOrderItem( "M10", shifts[3], dictionary );

            dashboardItemM11.ShiftItemsCurrent = DictionaryToProductionOrderItem( "M11", shifts[0], dictionary );
            dashboardItemM11.ShiftItemsMinus1 = DictionaryToProductionOrderItem( "M11", shifts[1], dictionary );
            dashboardItemM11.ShiftItemsMinus2 = DictionaryToProductionOrderItem( "M11", shifts[2], dictionary );
            dashboardItemM11.ShiftItemsMinus3 = DictionaryToProductionOrderItem( "M11", shifts[3], dictionary );

            dashboardItemM49.ShiftItemsCurrent = DictionaryToProductionOrderItem( "M49", shifts[0], dictionary );
            dashboardItemM49.ShiftItemsMinus1 = DictionaryToProductionOrderItem( "M49", shifts[1], dictionary );
            dashboardItemM49.ShiftItemsMinus2 = DictionaryToProductionOrderItem( "M49", shifts[2], dictionary );
            dashboardItemM49.ShiftItemsMinus3 = DictionaryToProductionOrderItem( "M49", shifts[3], dictionary );

            return new LaborDashboardViewModel
            {
                DashboardItemM10 = dashboardItemM10,
                DashboardItemM11 = dashboardItemM11,
                DashboardItemM49 = dashboardItemM49
            };

            #endregion
        }

        private ICollection<ProductionOrderItem> DictionaryToProductionOrderItem( String machine, ShiftSchedule shift, Dictionary<String, List<TestSheet>> dictionary )
        {
            if ( dictionary.ContainsKey( machine ) )
            {
                var sheets = dictionary[machine].Where( sheet => ShiftHelper.DateExistsInShift( sheet.CreatedDateTime, shift ) )
                                                .ToList();
                if ( sheets.Count != 0 )
                    return LaborDashboardHelper.ToProductionOrderItems( sheets );
            }
            return new List<ProductionOrderItem>();
        }
    }
}