﻿using System;
using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing the bll for the settings
    /// </summary>
    public interface ISettingsBll
    {
        /// <summary>
        ///     Query for all main modules to edit their settings.
        /// </summary>
        /// <returns>All modules with type main o</returns>
        IEnumerable<Module> AllVisibleModulesForRoles();

        /// <summary>
        /// Updates the Module visability in the db
        /// </summary>
        /// <param name="id">the id of the module</param>
        /// <param name="visability">the visability of the module</param>
        void UpdateModuleVisability( Int32 id, Boolean visability );
    }
}