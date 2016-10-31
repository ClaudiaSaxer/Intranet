using System;
using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Labor.Definition.Bll
{
    /// <summary>
    ///     Interface representing the bll for the labor home
    /// </summary>
    public interface ILaborHomeBll
    {
        /// <summary>
        ///     Query for all labor modules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All labor modules for the given roles</returns>
        IEnumerable<Module> AllLaborModulesForRoles(IEnumerable<String> rolenames);
    }
}
