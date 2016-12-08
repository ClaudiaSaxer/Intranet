using System;
using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Labor.Definition
{
    /// <summary>
    ///     Interface representing the bll for the labor home
    /// </summary>
    public interface ILaborHome
    {
        /// <summary>
        ///     Query for all labor modules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All labor modules for the given roles</returns>
        IEnumerable<Module> AllLaborModulesForRoles( IEnumerable<String> rolenames );
    }
}