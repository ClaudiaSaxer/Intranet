using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        ///     Query for all Modules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All Modules for the given roles</returns>
        public IQueryable<MainModule> AllMainModulesForRoles( IEnumerable<String> rolenames )
        {
          //  MainModuleRepository.GetAll().Where(  module => module.Roles.c  )            
            throw new NotImplementedException();
        }

        #endregion
    }
}