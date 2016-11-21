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
    }
}