#region Usings

using System;
using System.Collections.Generic;
using Intranet.Model;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing the bll for the navigation
    /// </summary>
    public interface INavigationBll
    {
        /// <summary>
        ///     Query for all SettingsModules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All Setting Modules for the given roles</returns>
        IEnumerable<Module> AllSettingsForRoles( IEnumerable<String> rolenames );

        /// <summary>
        ///     Query for all MainModules for a given roles.
        /// </summary>
        /// <param name="rolenames">The name of the roles the user has.</param>
        /// <returns>All MainModules for the given roles</returns>
        IEnumerable<Module> AllVisibleMainModulesForRoles( IEnumerable<String> rolenames );
    }
}