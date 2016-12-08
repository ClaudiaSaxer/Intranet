using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the bll of the navigation.
    /// </summary>
    public class NavigationBll : INavigationBll
    {
        #region Properties

        /// <summary>
        ///     Repository for Roles
        /// </summary>
        public IGenericRepository<Role> RoleRepository { get; set; }

        #endregion

        /// <summary>
        ///     Query for all SettingsModules for a given roles.
        /// </summary>
        /// <param name="roleNames">The name of the roles the user has.</param>
        /// <returns>All Settings for the given roles</returns>
        public IEnumerable<Module> AllSettingsForRoles( IEnumerable<String> roleNames )
        {
            var modules = RoleRepository.GetAll()
                                        .Where( role => roleNames.Any( n => n.Contains( role.Name ) ) )
                                        .SelectMany( role => role.Modules )
                                        .Where( module => module.Type == ModuleType.Setting )
                                        .Distinct()
                                        .ToList();
            return modules;
        }

        /// <summary>
        ///     Query for all Modules for a given roles.
        /// </summary>
        /// <param name="roleNames">The name of the roles the user has.</param>
        /// <returns>All Modules for the given roles</returns>
        public IEnumerable<Module> AllVisibleMainModulesForRoles( IEnumerable<String> roleNames )
        {
            var modules = RoleRepository.GetAll()
                                        .Where( role => roleNames.Any( n => n.Contains( role.Name ) ) )
                                        .SelectMany( role => role.Modules )
                                        .Where( module => ( module.Visible == true ) && ( module.Type == ModuleType.Main ) )
                                        .Distinct()
                                        .ToList();
            return modules;
        }
    }
}