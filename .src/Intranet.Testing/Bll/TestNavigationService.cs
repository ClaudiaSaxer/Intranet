using System.Collections.Generic;
using FluentAssertions;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using IntranetTestEnvironment;
using Moq;
using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    ///     Test Class for NavigationService
    /// </summary>
    public class TestNavigationService
    {
        /// <summary>
        ///     Passing Test
        /// </summary>
        [Fact]
        public void AllVisibleMainModulesForRoles()
        {
            var m1 = "Auf dem Tisch stehen";
            var m2 = "Gummibärli esse";
            var m3 = "Kaffii trinkä";
            var m4 = "So Istellige halt";

            var invoked = false;
            var navigationBllMock = 
                MockHelperBll.GetNavigationBll(
                    x => new List<Module> { new Module { Visible = false, Name = m1 }, new Module { Name = m2, Visible = true }, new Module { Name = m3, Visible = true } },
                    y => new List<Module> { new Module { Name = m4, Visible = true } } );

            var serviceBaseMock = MockHelperServiceBase.GetServiceBase();           

            var target = new NavigationService( new NLogLoggerFactory(),serviceBaseMock )
            {
                NavigationBll = navigationBllMock
            };
            
            var actual = target.GetNavigationViewModel();

            invoked.Should()
                   .BeTrue( "My class should call this method" );

            actual.MainModules.GetEnumerator()
                  .Should()
                  .NotBeNull( "because there are visible modules" );
            var mainmodules = actual.MainModules.GetEnumerator();
            mainmodules.Current.Name.Should()
                       .Be( m2 );
            mainmodules.Current.Visible.Should()
                       .BeTrue( "because all modules for this method must be visible" );
            mainmodules.MoveNext()
                       .Should()
                       .BeTrue( "because all modules for this method must be visible" );
            mainmodules.Current.Name.Should()
                       .Be( m3 );
            mainmodules.Current.Visible.Should()
                       .BeTrue( "because all modules for this method must be visible" );
            mainmodules.MoveNext()
                       .Should()
                       .BeFalse( "because no more modules exist" );

            actual.SettingModules.GetEnumerator()
                  .Should()
                  .NotBeNull( "because there is visible setting module" );
            var settingmodules = actual.SettingModules.GetEnumerator();
            settingmodules.Current.Name.Should()
                          .Be( m4 );
            settingmodules.Current.Visible.Should()
                          .BeTrue( "because settings module must be true" );
            settingmodules.MoveNext()
                          .Should()
                          .BeFalse( "because no more modules exist" );
        }
    }
}