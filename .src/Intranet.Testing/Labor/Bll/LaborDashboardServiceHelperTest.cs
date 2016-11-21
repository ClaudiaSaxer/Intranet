#region Usings

using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model.labor;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for Labor Dashboard Service Helper
    /// </summary>
    public class LaborDashboardServiceHelperTest
    {
        /// <summary>
        ///     Calc Rw type test
        /// </summary>
        [Fact]
        public void CalcRwTypeTest()
        {
            var target = new LaborDashboardHelper( new NLogLoggerFactory() );
            target.CalcRwType( RwType.Ok, RwType.Better )
                  .Should()
                  .Be( RwType.Better );
            target.CalcRwType( RwType.Ok, RwType.Worse )
                  .Should()
                  .Be( RwType.Worse );
            target.CalcRwType( RwType.SomethingWorse, RwType.Worse )
                  .Should()
                  .Be( RwType.Worse );
            target.CalcRwType( RwType.Ok, RwType.SomethingWorse )
                  .Should()
                  .Be( RwType.SomethingWorse );
        }

        /// <summary>
        ///    ToDashboardInfos test
        /// </summary>
        [Fact]
        public void ToDashboardInfosTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToDashboardInfos(null);
        }
        /// <summary>
        ///    ToDashboardInfosBabyDiapers test
        /// </summary>
        [Fact]
        public void ToDashboardInfosBabyDiapersTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToDashboardInfosBabyDiapers( null );
        }
      
        /// <summary>
        ///    ToDashboardInfosIncontinencePad test
        /// </summary>
        [Fact]
        public void ToDashboardInfosIncontinencePadTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToDashboardInfosIncontinencePad( null );
        }
        /// <summary>
        ///    ToDashboardNote test
        /// </summary>
        [Fact]
        public void ToDashboardNoteTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToDashboardNote( null );
        }
        /// <summary>
        ///    ToProductionOrderItem test
        /// </summary>
        [Fact]
        public void ToProductionOrderItemTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToProductionOrderItem( null );
        }
        /// <summary>
        ///    ToProductionOrderItems test
        /// </summary>
        [Fact]
        public void ToProductionOrderItemsTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToProductionOrderItems( null );
        }
        /// <summary>
        ///    ToRwTypeAll test
        /// </summary>
        [Fact]
        public void ToRwTypeAllTest()
        {
            var target = new LaborDashboardHelper(new NLogLoggerFactory());
            target.ToRwTypeAll( null );
        }
    }
}