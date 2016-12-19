#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Model;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the bll of the babydiaper labor home.
    /// </summary>
    public class BabyDiaperLaborHomeBll : ILaborHome
    {
        #region Properties

        /// <summary>
        ///     RoleRepository
        /// </summary>
        public IGenericRepository<Role> RoleRepository { get; set; }

        #endregion

        #region Implementation of ILaborHome

        /// <summary>
        ///     Query for all labor modules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All labor modules for the given roles</returns>
        public IEnumerable<Module> AllLaborModulesForRoles( IEnumerable<String> rolenames )
        {
            var modules = RoleRepository.GetAll()
                                        .Where( role => rolenames.Any( n => n.Contains( role.Name ) ) )
                                        .SelectMany( role => role.Modules )
                                        .Where( module => ( module.Type == ModuleType.Sub ) && module.AreaName.Equals( "Labor" ) )
                                        .Distinct()
                                        .ToList();
            return modules;
        }

        #endregion
    }
}