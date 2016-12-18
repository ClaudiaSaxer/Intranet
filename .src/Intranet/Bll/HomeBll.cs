#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Definition;
using Intranet.Model;

#endregion

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the bll of the home.
    /// </summary>
    public class HomeBll : IHomeBll
    {
        #region Properties

        /// <summary>
        ///     Repository for Roles
        /// </summary>
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
                                        .Where( module => ( module.Visible == true ) && ( ( module.Type == ModuleType.Setting ) || ( module.Type == ModuleType.Main ) ) )
                                        .Distinct()
                                        .ToList();
            return modules;
        }

        #endregion
    }
}