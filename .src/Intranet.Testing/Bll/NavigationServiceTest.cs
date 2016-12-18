#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using Xunit;

#endregion

namespace Intranet.Bll.Test
{
    /// <summary>
    ///     Test Class for NavigationService
    /// </summary>
    public class NavigationServiceTest
    {
        /// <summary>
        ///     Normal Passing Test with no value
        /// </summary>
        [Fact]
        public void AllVisibleMainModulesForRolesEmptyTest()
        {
            var navigationBllMock =
                MockHelperBll.GetNavigationBll(
                    new List<Module>(),
                    new List<Module>()
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new NavigationService( new NLogLoggerFactory() )
            {
                NavigationBll = navigationBllMock,
                Roles = rolesMock
            };

            var actual = target.GetNavigationViewModel();

            actual.MainModules.ToList()
                  .Count.Should()
                  .Be( 0 );

            actual.SettingModules.ToList()
                  .Count.Should()
                  .Be( 0 );
        }

        /// <summary>
        ///     Normal Passing Test
        /// </summary>
        [Fact]
        public void AllVisibleMainModulesForRolesNormalTest()
        {
            var m1 = "Auf dem Tisch stehen";
            var m2 = "Gummibärli esse";
            var m3 = "Kaffii trinkä";
            var m4 = "So Istellige halt";

            var navigationBllMock =
                MockHelperBll.GetNavigationBll(
                    new List<Module> { new Module { Name = m4, Visible = true } },
                    new List<Module>
                    {
                        new Module { Name = m1, Visible = true },
                        new Module { Name = m2, Visible = true },
                        new Module { Name = m3, Visible = true }
                    }
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new NavigationService( new NLogLoggerFactory() )
            {
                NavigationBll = navigationBllMock,
                Roles = rolesMock
            };

            var actual = target.GetNavigationViewModel();

            var mainmodules = actual.MainModules.ToList();
            mainmodules
                .Should()
                .NotBeNull( "because there are visible modules" );
            mainmodules.Count.Should()
                       .Be( 3 );
            mainmodules[0].Name.Should()
                          .Be( m1 );
            mainmodules[0].Visible.Should()
                          .BeTrue( "because all modules for this method must be visible" );
            mainmodules[1].Name.Should()
                          .Be( m2 );
            mainmodules[1].Visible.Should()
                          .BeTrue( "because all modules for this method must be visible" );
            mainmodules[2].Name.Should()
                          .Be( m3 );
            mainmodules[2].Visible.Should()
                          .BeTrue( "because all modules for this method must be visible" );

            var settingmodules = actual.SettingModules.ToList();

            settingmodules
                .Should()
                .NotBeNull( "because there is visible setting module" );
            settingmodules.Count.Should()
                          .Be( 1 );
            settingmodules[0].Name.Should()
                             .Be( m4 );
            settingmodules[0].Visible.Should()
                             .BeTrue( "because settings module must be true" );
        }
    }
}