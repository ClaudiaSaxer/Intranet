using System;
using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Definition.Bll
{
    /// <summary>
    ///     Interface representing the bll for the navigation
    /// </summary>
    public interface INavigationBll
    {
        /// <summary>
        ///     Query for all MainModules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All MainModules for the given roles</returns>
        IEnumerable<MainModule> AllVisibleMainModulesForRoles( IEnumerable<String> rolenames );
    }
}