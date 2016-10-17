using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extend;
using Intranet.Definition;
using Intranet.Definition.Bll;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the bll of the navigation.
    /// </summary>
    internal class NavigationBll : INavigationBll
    {
        /// <summary>
        /// </summary>
        public IGenericRepository<MainModule> MainModuleRepository { get; set; }
        #region Implementation of INavigationBll
        public IGenericRepository<Role> RoleRepository { get; set; }

        /// <summary>
        ///     Query for all Modules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All Modules for the given roles</returns>
        public IQueryable<MainModule> AllVisibleMainModulesForRoles( IEnumerable<String> rolenames )
        {
            
            var roles = RoleRepository.GetAll().Where( role =>role.Name.IsIn( rolenames )  ).AsEnumerable();
            var modules = MainModuleRepository.GetAll().Where( module => module.Roles.IsIn( roles ) ).Where( module => module.Visible );
            return modules;
        }

        #endregion
    }
}