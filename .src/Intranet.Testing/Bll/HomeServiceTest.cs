using System;
using System.Collections.Generic;
using System.Linq;
using Extend;
using FluentAssertions;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    ///     Test Class for HomeService
    /// </summary>
    public class HomeServiceTest
    {

        /// <summary>
        ///     Normal Passing Test with 2 model
        /// </summary>
        [Fact]
        public void GetHomeViewModel2ModelTest()
        {
            var n1 = "TheMightyOne";
            var n2 = "TheMightyTwo";

            var homebllmock =
                MockHelperBll.GetHomeBll(
                    new List<Module>
                    {
                        new Module { Name = n1 }
                        ,
                        new Module { Name = n2 }
                    }
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new HomeService( new NLogLoggerFactory() )
            {
                HomeBll = homebllmock,
                Roles = rolesMock
            };

            var actual = target.GetHomeViewModel();

            actual.Modules.ToList()
                  .Count.Should()
                  .Be( 2 );

            actual.Modules.ToList()[0].Name.Should()
                  .Be( n1 );
            actual.Modules.ToList()[1].Name.Should()
                  .Be( n2 );
        }

        /// <summary>
        ///     Normal Passing Test with no value
        /// </summary>
        [Fact]
        public void GetHomeViewModelEmptyTest()
        {
            var homebllmock =
                MockHelperBll.GetHomeBll(
                    new List<Module>()
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new HomeService( new NLogLoggerFactory() )
            {
                HomeBll = homebllmock,
                Roles = rolesMock
            };

            var actual = target.GetHomeViewModel();

            actual.Modules.ToList()
                  .Count.Should()
                  .Be( 0 );
        }

        /// <summary>
        ///     Normal Passing Test with 1 model
        /// </summary>
        [Fact]
        public void GetHomeViewModelSingleModelTest()
        {
            var name = "fancy stuff";

            var homebllmock =
                MockHelperBll.GetHomeBll(
                    new List<Module>
                    {
                        new Module
                        {
                            Name = name
                        }
                    }
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new HomeService( new NLogLoggerFactory() )
            {
                HomeBll = homebllmock,
                Roles = rolesMock
            };

            var actual = target.GetHomeViewModel();

            actual.Modules.ToList()
                  .Count.Should()
                  .Be( 1 );
            actual.Modules.ToList()[0].Name.Should()
                  .Be( name );
        }
    }
}