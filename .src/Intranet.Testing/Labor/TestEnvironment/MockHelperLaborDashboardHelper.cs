#region Usings

using System.Collections.Generic;
using Intranet.Labor.Definition;
using Intranet.Labor.Model;
using Intranet.Labor.ViewModel;
using Moq;

#endregion

namespace Intranet.Labor.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for labor Dashboard helper
    /// </summary>
    public static class MockHelperLaborDashboardHelper
    {
        /// <summary>
        ///     Creates a mock for the interface labordashboar helper
        /// </summary>
        /// <param name="notes">the dashboard notes to create</param>
        /// <param name="rwType">the rwtype to return for all methods calculating the cumulated rw type</param>
        /// <param name="dashboardInfos">the dashboard infos created by all methods ToDashbardInfo...</param>
        /// <param name="productionOrderItem">production order item for toProductionOtderItem</param>
        /// <param name="productionOrderItems">collection of production order items for toProductionOrderItems</param>
        /// <returns>A mock for the labor dashboard helper</returns>
        public static ILaborDashboardHelper GetLaborDashboardHelper( List<DashboardNote> notes = null,
                                                                     RwType rwType = RwType.Ok,
                                                                     List<DashboardInfo> dashboardInfos = null,
                                                                     ProductionOrderItem productionOrderItem = null,
                                                                     ICollection<ProductionOrderItem> productionOrderItems = null )
        {
            var mock = new Mock<ILaborDashboardHelper>
            {
                Name = "MockHelperLaborDashboardHelper.GetLaborDashboardHelper",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.CalcRwType( It.IsAny<RwType>(), It.IsAny<RwType>() ) )
                .Returns( rwType );
            mock.Setup( x => x.ToRwTypeAll( It.IsAny<IEnumerable<TestValue>>() ) )
                .Returns( rwType )
                ;
            mock.Setup( x => x.ToDashboardInfos( It.IsAny<IEnumerable<TestValue>>() ) )
                .Returns( dashboardInfos );
            mock.Setup( x => x.ToDashboardNote( It.IsAny<IEnumerable<TestValue>>() ) )
                .Returns( notes );
            mock.Setup( x => x.ToDashboardInfosBabyDiapers( It.IsAny<TestValue>() ) )
                .Returns( dashboardInfos );
            mock.Setup( x => x.ToDashboardInfosIncontinencePad( It.IsAny<TestValue>() ) )
                .Returns( dashboardInfos );
            mock.Setup( x => x.ToProductionOrderItem( It.IsAny<TestSheet>() ) )
                .Returns( productionOrderItem );
            mock.Setup( x => x.ToProductionOrderItems( It.IsAny<ICollection<TestSheet>>() ) )
                .Returns( productionOrderItems );

            return mock.Object;
        }
    }
}