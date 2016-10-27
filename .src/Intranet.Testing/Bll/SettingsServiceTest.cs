using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using Intranet.ViewModel;
using Xunit;

namespace Intranet.Bll.Test
{
    /// <summary>
    ///     Test Class for SettingsService
    /// </summary>
    public class SettingsServiceTest
    {
        /// <summary>
        ///     Normal Passing Test with one model
        /// </summary>
        [Fact]
        public void GetSettingsViewModel1ModelTest()
        {
            var n = "Gireizlä";
            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>
                    {
                        new Module
                        {
                            Name = n
                        }
                    }
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.GetSettingsViewModel();

            var modules =
                actual.ModuleSettings.ToList();
            modules
                .Count.Should()
                .Be( 1 );
            modules.First()
                   .Name.Should()
                   .Be( n );
        }

        /// <summary>
        ///     Normal Passing Test with 2 Models
        /// </summary>
        [Fact]
        public void GetSettingsViewModel2ModelTest()
        {
            var n1 = "TheOne";
            var n2 = "TheOnly";
            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>
                    {
                        new Module
                        {
                            Name = n1
                        },
                        new Module { Name = n2 }
                    }
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.GetSettingsViewModel();

            var modules =
                actual.ModuleSettings.ToList();
            modules
                .Count.Should()
                .Be( 2 );
            modules.First()
                   .Name.Should()
                   .Be( n1 );
            modules[1].Name.Should()
                      .Be( n2 );
        }

        /// <summary>
        ///     Normal Passing Test with no value
        /// </summary>
        [Fact]
        public void GetSettingsViewModelEmptyTest()
        {
            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>()
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.GetSettingsViewModel();

            actual.ModuleSettings.ToList()
                  .Count.Should()
                  .Be( 0 );
        }

        /// <summary>
        ///     Normal Passing Test with no models
        /// </summary>
        [Fact]
        public void UpdateModuleSettingEmptyTest()
        {
            var n1 = "Changer";
            var id1 = 0;
            var vis = true;
            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>()
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.UpdateModuleSetting( new ModuleSetting { Visible = vis, Name = n1, Id = id1 } );

            actual.Should()
                  .BeNull( "because no modules exist" );
        }

        /// <summary>
        ///     Normal Passing Test with models and no match
        /// </summary>
        [Fact]
        public void UpdateModuleSettingIdNotFoundTest()
        {
            var n1 = "Changer";
            var id1 = 0;
            var vis = true;

            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>( new List<Module> { new Module { Name = "Bla", ModuleId = 66, Visible = true } } )
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.UpdateModuleSetting( new ModuleSetting { Visible = vis, Name = n1, Id = id1 } );

            actual.Should()
                  .BeNull( "because no modules exist" );
        }

        /// <summary>
        ///     Normal Passing Test with 1 model and update
        ///     Before not visible, after visible
        /// </summary>
        [Fact]
        public void UpdateModuleSettingNormal1Test()
        {
            var n = "Changer";
            var id = 0;
            var visbefore = false;
            var visafter = true;

            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>( new List<Module> { new Module { Name = n, ModuleId = id, Visible = visbefore } } )
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.UpdateModuleSetting( new ModuleSetting { Visible = visafter, Name = n, Id = id } );

            actual.Should()
                  .NotBeNull( "because model exist" );
            actual.Visible.Should()
                  .Be( visafter );
            actual.ModuleId.Should()
                  .Be( id );
            actual.Name.Should()
                  .Be( n );
        }

        /// <summary>
        ///     Normal Passing Test with 1 model and update
        ///     Before  visible, after not visible
        /// </summary>
        [Fact]
        public void UpdateModuleSettingNormal2Test()
        {
            var n = "Changer";
            var id = 0;
            var visbefore = true;
            var visafter = false;

            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>( new List<Module> { new Module { Name = n, ModuleId = id, Visible = visbefore } } )
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.UpdateModuleSetting( new ModuleSetting { Visible = visafter, Name = n, Id = id } );

            actual.Should()
                  .NotBeNull( "because model exist" );
            actual.Visible.Should()
                  .Be( visafter );
            actual.ModuleId.Should()
                  .Be( id );
            actual.Name.Should()
                  .Be( n );
        }

        /// <summary>
        ///     Normal Passing Test with 2 model and update
        ///     Before  visible, after not visible
        /// </summary>
        [Fact]
        public void UpdateModuleSettingNormal3Test()
        {
            var n = "Changer";
            var n2 = "Stay";
            var id = 0;
            var id2 = 44;
            var visbefore = true;
            var visafter = false;
            var vis2 = true;

            var settingsMock =
                MockHelperBll.GetSettingsBll(
                    new List<Module>( new List<Module>
                                      {
                                          new Module { Name = n, ModuleId = id, Visible = visbefore },
                                          new Module { Name = n2, ModuleId = id2, Visible = vis2 }
                                      } )
                );

            var rolesMock = MockHelperRoles.GetRoles( new List<String>() );

            var target = new SettingsService( new NLogLoggerFactory() )
            {
                SettingsBll = settingsMock,
                Roles = rolesMock
            };

            var actual = target.UpdateModuleSetting( new ModuleSetting { Visible = visafter, Name = n, Id = id } );

            actual.Should()
                  .NotBeNull( "because model exist" );
            actual.Visible.Should()
                  .Be( visafter );
            actual.ModuleId.Should()
                  .Be( id );
            actual.Name.Should()
                  .Be( n );
        }
    }
}