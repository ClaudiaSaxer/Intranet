using System;
using System.Collections.Generic;
using Intranet.Common;
using Intranet.Model;
using Moq;

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for bll
    /// </summary>
    public static class MockHelperBll
    {
        /// <summary>
        ///     A mock for HomeBll
        /// </summary>
        /// <param name="modules">Modules returned by AllVisibleModulesForRoles</param>
        /// <returns></returns>
        public static IHomeBll GetHomeBll( IEnumerable<Module> modules)
        {
            var mock = new Mock<IHomeBll>
            {
                Name = "MockHelper.GetHomeBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.AllVisibleModulesForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( modules );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for NavigationBll
        /// </summary>
        /// <param name="settings">the setting modules</param>
        /// <param name="modules">the main modules</param>
        /// <returns></returns>
        public static INavigationBll GetNavigationBll(
            IEnumerable<Module> settings = null,
            IEnumerable<Module> modules = null
        )
        {
            var mock = new Mock<INavigationBll>
            {
                Name = "MockHelper.GetNavigationBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.AllSettingsForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<String> s ) => settings );

            mock.Setup( x => x.AllVisibleMainModulesForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<String> m ) => modules );
            return mock.Object;
        }

        /// <summary>
        ///     A mock for SettingsBll
        /// </summary>
        /// <param name="getSettingsBllModuleFunc">Func for AllVisibleMainModules for SettingsBll</param>
        /// <param name="getSettingsBllModuleCallback">Callback for AllVisibleMainModules for SettingsBll</param>
        /// <param name="getSettingsBllVisabilityCallback">Callback for UpdateModuleVisability for SettingsBll</param>
        /// <param name="getSettingsBllVisabilityFunc">Func for UpdateModuleVisability for SettingsBll</param>
        /// <returns></returns>
        public static ISettingsBll GetSettingsBll( Func<IEnumerable<Module>> getSettingsBllModuleFunc = null,
                                                   Action<String> getSettingsBllModuleCallback = null,
                                                   Func<Int32, Boolean, Module> getSettingsBllVisabilityFunc = null,
                                                   Action<Int32, Boolean> getSettingsBllVisabilityCallback = null )
        {
            var mock = new Mock<ISettingsBll>
            {
                Name = "MockHelper.GetSettingsBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.AllVisibleMainModules() )
                .Returns( () => getSettingsBllModuleFunc?.Invoke() )
                .Callback( ( String s ) => getSettingsBllModuleCallback?.Invoke( s ) );

            mock.Setup( x => x.UpdateModuleVisability( It.IsAny<Int32>(), It.IsAny<Boolean>() ) )
                .Returns( ( Int32 i, Boolean v ) => getSettingsBllVisabilityFunc?.Invoke( i, v ) )
                .Callback( ( Int32 i, Boolean v ) => getSettingsBllVisabilityCallback?.Invoke( i, v ) );

            return mock.Object;
        }
    }
}