using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the bll of the home.
    /// </summary>
    internal class HomeBll : IHomeBll
    {
        #region Properties

        public IGenericRepository<Role> RoleRepository { get; set; }

        #endregion

        #region Implementation of IHomeBll

        /// <summary>
        ///     Query for all modules for a given roles. Modules with type main or settings
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All modules with type main or settings for the given roles</returns>
        public IEnumerable<Module> AllVisibleModulesForRoles( IEnumerable<String> rolenames )
        {
            var modules = RoleRepository.GetAll()
                                        .Where( role => rolenames.Any( n => n.Contains( role.Name ) ) )
                                        .SelectMany( role => role.Modules )
                                        .Where( module => ( module.Type == ModuleType.Setting ) || ( module.Type == ModuleType.Main ) )
                                        .Distinct()
                                        .ToList();
            return modules;
        }

        #endregion
    }
}