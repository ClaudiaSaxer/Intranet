using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
                .Returns( ( IEnumerable<Module> modules ) => getHomeBllFunc?.Invoke( modules ) )
                .Callback( ( IEnumerable<Module> modules ) => getHomeBllCallback?.Invoke( modules ) );

            return mock.Object;
        }

        /// <summary>
        /// A mock for NavigationBll
        /// </summary>
        /// <param name="getNavigationBllSettingsFunc">Func for AllSettingsForRoles for NavigationBll</param>
        /// <param name="getNavigationBllSettingsCallback">Callback for AllSettingsForRoles for NavigationBll</param>
        /// <param name="getNavigationBllModuleFunc">Func for AllVisibleMainModulesForRoles for NavigationBll</param>
        /// <param name="getNavigationBllModuleCallback">Callback for AllVisibleMainModulesForRoles for NavigationBll</param>
        /// <returns></returns>
        public static INavigationBll GetNavigationBll( 
            Func<IEnumerable<Module>, IEnumerable<Module>> getNavigationBllSettingsFunc = null,
            Action<IEnumerable<Module>> getNavigationBllSettingsCallback = null, Func<IEnumerable<Module>, IEnumerable<Module>> getNavigationBllModuleFunc = null,
            Action<IEnumerable<Module>> getNavigationBllModuleCallback = null)
        {
            var mock = new Mock<INavigationBll>
            {
                Name = "MockHelper.GetNavigationBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.AllSettingsForRoles( It.IsAny<IEnumerable<String>>() ) )
                .Returns( ( IEnumerable<Module> settingmodules ) => getNavigationBllSettingsFunc?.Invoke( settingmodules ) )
                .Callback( ( IEnumerable<Module> settingmodules ) => getNavigationBllSettingsCallback?.Invoke( settingmodules ) );

            mock.Setup( x => x.AllVisibleMainModulesForRoles(It.IsAny<IEnumerable<String>>()))
                .Returns((IEnumerable<Module> settingmodules) => getNavigationBllModuleFunc?.Invoke(settingmodules))
                .Callback((IEnumerable<Module> settingmodules) => getNavigationBllModuleCallback?.Invoke(settingmodules));
            return mock.Object;
        }

        /// <summary>
        /// A mock for SettingsBll
        /// </summary>
        /// <param name="getSettingsBllFunc">Func for SettingsBll</param>
        /// <param name="getSettingsBllCallback">Callback for SettingsBll</param>
        /// <returns></returns>
        public static ISettingsBll GetSettingsBll( Func<IEnumerable<Module>, IEnumerable<Module>> getSettingsBllFunc = null, Action<IEnumerable<Module>> getSettingsBllCallback = null )
        {
            var mock = new Mock<ISettingsBll>
            {
                Name = "MockHelper.GetSettingsBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup( x => x.AllVisibleMainModules() )
                .Returns((IEnumerable<Module> mainmodules ) => getSettingsBllFunc?.Invoke(mainmodules))
                .Callback( (IEnumerable<Module> mainmodules) => getSettingsBllCallback?.Invoke( mainmodules ) );

            return mock.Object;
        }
    }
}