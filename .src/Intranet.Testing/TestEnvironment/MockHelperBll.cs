using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Definition;
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
        public static IHomeBll GetHomeBll( IEnumerable<Module> modules )
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

        /// <param name="modules">All Modules</param>
        /// <returns></returns>
        public static ISettingsBll GetSettingsBll( IEnumerable<Module> modules )
        {
            var mock = new Mock<ISettingsBll>
            {
                Name = "MockHelper.GetSettingsBll",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( x => x.AllVisibleMainModules() )
                .Returns( modules );

            mock.Setup( x => x.UpdateModuleVisability( It.IsAny<Int32>(), It.IsAny<Boolean>() ) )
                .Returns( ( Int32 i, Boolean v ) =>
                          {
                              var m = modules.Where( module => module.ModuleId == i )
                                             .FirstOrDefault();
                              if ( m != null )
                                  m.Visible = v;
                              return m;
                          } );

            return mock.Object;
        }

        /// <summary>
        ///     A mock for checkdisable bll
        /// </summary>
        /// <param name="module">The returned module</param>
        /// <returns>a generic repository for production orders</returns>
        public static ICheckDisableBll GetCheckDisableBll(Module module)
        {
            var mock = new Mock<ICheckDisableBll>
            {
                Name = "MockHelper.GetCheckDisableBll",
                DefaultValue = DefaultValue.Mock
            };
            mock.Setup(x => x.GetModule(It.IsAny<String>()))
                .Returns(module);

            return mock.Object;
        }

        /// <summary>
        ///     A mock for GenericRepository
        /// </summary>
        /// <param name="modules">Queryable modules returned by GetAll</param>
        /// <returns>a generic repository for roles</returns>
        public static IGenericRepository<Module> GetAllModules(IQueryable<Module> modules)
        {
            var mock = new Mock<IGenericRepository<Module>>
            {
                Name = "MockHelper.GetAllModules",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup(x => x.GetAll())
                .Returns(modules);

            return mock.Object;
        }
    }
}