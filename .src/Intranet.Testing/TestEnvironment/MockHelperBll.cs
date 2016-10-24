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
        /// <param name="getHomeBllFunc">Func for HomeBll</param>
        /// <param name="getHomeBllCallback">Callback for HomeBll</param>
        /// <returns></returns>
        public static IHomeBll GetHomeBll(
            Func<IEnumerable<Module>, IEnumerable<Module>> getHomeBllFunc = null,
            Action<IEnumerable<Module>> getHomeBllCallback = null )
        {
            var mock = new Mock<IHomeBll>
            {
                Name = "MockHelper.GetHomeBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.AllVisibleModulesForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<Module> m ) => getHomeBllFunc?.Invoke( m ) )
                .Callback( ( IEnumerable<Module> m ) => getHomeBllCallback?.Invoke( m ) );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for NavigationBll
        /// </summary>
        /// <param name="getNavigationBllSettingsFunc">Func for AllSettingsForRoles for NavigationBll</param>
        /// <param name="getNavigationBllSettingsCallback">Callback for AllSettingsForRoles for NavigationBll</param>
        /// <param name="getNavigationBllModuleFunc">Func for AllVisibleMainModulesForRoles for NavigationBll</param>
        /// <param name="getNavigationBllModuleCallback">Callback for AllVisibleMainModulesForRoles for NavigationBll</param>
        /// <returns></returns>
        public static INavigationBll GetNavigationBll(
            Func<IEnumerable<String>, IEnumerable<Module>> getNavigationBllSettingsFunc = null,
            Action<IEnumerable<String>> getNavigationBllSettingsCallback = null,
            Func<IEnumerable<String>, IEnumerable<Module>> getNavigationBllModuleFunc = null,
            Action<IEnumerable<String>> getNavigationBllModuleCallback = null )
        {
            var mock = new Mock<INavigationBll>
            {
                Name = "MockHelper.GetNavigationBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.AllSettingsForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<String> s ) => getNavigationBllSettingsFunc?.Invoke( s ) )
                .Callback( (IEnumerable<String> s) => getNavigationBllSettingsCallback?.Invoke( s ) );

            mock.Setup( x => x.AllVisibleMainModulesForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<String> m ) => getNavigationBllModuleFunc?.Invoke( m ) )
                .Callback( (IEnumerable<String> m) => getNavigationBllModuleCallback?.Invoke( m ) );
            return mock.Object;
        }

        /// <summary>
        ///     A mock for SettingsBll
        /// </summary>
        /// <param name="getSettingsBllModuleFunc">Func for AllVisibleMainModules for SettingsBll</param>
        /// <param name="getSettingsBllModuleCallback">Callback for AllVisibleMainModules for SettingsBll</param>
        /// <param name="getSettingsBllVisabilityCallback">Callback for UpdateModuleVisability for SettingsBll</param>
        /// <returns></returns>
        public static ISettingsBll GetSettingsBll( Func<IEnumerable<Module>> getSettingsBllModuleFunc = null,
                                                   Action<String> getSettingsBllModuleCallback = null,
                                                   Action<Int32, Boolean> getSettingsBllVisabilityCallback = null )
        {
            var mock = new Mock<ISettingsBll>
            {
                Name = "MockHelper.GetSettingsBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.AllVisibleMainModules() )
                .Returns( () => getSettingsBllModuleFunc?.Invoke() )
                .Callback( (String s) => getSettingsBllModuleCallback?.Invoke(s) );

            mock.Setup( x => x.UpdateModuleVisability( It.IsAny<Int32>(), It.IsAny<Boolean>() ) )
                .Callback( ( Int32 i, Boolean v ) => getSettingsBllVisabilityCallback?.Invoke( i, v ) );

            ;

            return mock.Object;
        }
    }
}