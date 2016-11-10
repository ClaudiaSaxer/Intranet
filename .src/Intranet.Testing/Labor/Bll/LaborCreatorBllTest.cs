using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Labor.Model;
using Intranet.Labor.Model.labor;
using Intranet.Model;
using Intranet.TestEnvironment;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;
using MockHelperBll = Intranet.Labor.TestEnvironment.MockHelperBll;

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for LaborCreatorBll
    /// </summary>
    public class LaborCreatorBllTest
    {

            /// <summary>
            ///     Normal Passing Test with Matching role but no Matching Submodule
            /// </summary>
            [Fact]
            public void xx()
            {
            var productionOrderListQuery = new List<ProductionOrder>();

            var productionOrderRepository = MockHelperBll.GetAllProductionOrders(productionOrderListQuery.AsQueryable());


            var target = new LaborCreatorBll(new NLogLoggerFactory()  )
            {
ProductionOrderRepository = null,
                ShiftScheduleRepository = null,
                TestSheetRepository = null

            };

          
           
       

        
        }


    }