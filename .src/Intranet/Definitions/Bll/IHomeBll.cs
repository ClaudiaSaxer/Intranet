using System;
using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing the bll for the home
    /// </summary>
    public interface IHomeBll
    {
        /// <summary>
        ///     Query for all modules for a given roles. Modules with type main or settings
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All modules with type main or settings for the given roles</returns>
        IEnumerable<Module> AllVisibleModulesForRoles( IEnumerable<String> rolenames );

   
    }
}
