#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Labor.Bll;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using Xunit;
using MockHelperBll = Intranet.Labor.TestEnvironment.MockHelperBll;

#endregion

namespace Intranet.Labor.Bll.Test
{
    /// <summary>
    ///     Test Class for LaborHomeService
    /// </summary>
    public class LaborHomeServiceTest
    {
        /// <summary>
        ///     Normal Passing Test with no value
        /// </summary>
        [Fact]
        public void GetLaborHomeViewModelEmptyTest()
        {
            var laborhomebllmock =
                MockHelperBll.GetLaborHomeBll(
                    new List<Module>()
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new LaborHomeService( new NLogLoggerFactory() )
            {
                LaborHomeBll = laborhomebllmock,
                Roles = rolesMock
            };

            var actual = target.GetLaborHomeViewModel();

            Assert.Equal( 0, actual.Modules.Count() );
        }

        /// <summary>
        ///     Normal Passing Test with 2 model
        /// </summary>
        [Fact]
        public void GetLaborHomeViewModel2ModelTest()
        {
            const String moduleName1 = "Dashboard";
            const String moduleName2 = "Input";
            var laborhomebllmock =
                MockHelperBll.GetLaborHomeBll(
                    new List<Module>
                    {
                        new Module { Name = moduleName1 }
                        ,
                        new Module { Name = moduleName2 }
                    }
                );

            var rolesMock = MockHelperRoles.GetRoles(new List<String>());

            var target = new LaborHomeService(new NLogLoggerFactory())
            {
                LaborHomeBll = laborhomebllmock,
                Roles = rolesMock
            };

            var actual = target.GetLaborHomeViewModel();

            Assert.Equal(2, actual.Modules.Count());
            Assert.Equal(moduleName1, actual.Modules.First().Name);
            Assert.Equal(moduleName2, actual.Modules.ToList()[1].Name);
        }
    }
}