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
    internal class NavigationBll : INavigationBll
    {
        #region Properties

        /// <summary>
        /// </summary>
        public IGenericRepository<MainModule> MainModuleRepository { get; set; }

        #endregion

        #region Implementation of INavigationBll

        public IGenericRepository<Role> RoleRepository { get; set; }

        /// <summary>
        ///     Query for all Modules for a given roles.
        /// </summary>
        /// <param name="roleNames">The name of the roles the user has.</param>
        /// <returns>All Modules for the given roles</returns>
        public IEnumerable<MainModule> AllVisibleMainModulesForRoles( IEnumerable<String> roleNames )
        {
            var modules = RoleRepository.GetAll()
                                        .Where( role => roleNames.Any( n => n.Contains( role.Name ) ) )
                                        .SelectMany( role => role.MainModules )
                                        .Distinct()
                                        .ToList();
            return modules;
        }

        #endregion
    }
}