using System;
using System.Collections.Generic;
using System.Linq;
using Intranet.Common;
using Intranet.Definition;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the bll of the settings.
    /// </summary>
    public class SettingsBll : ISettingsBll
    {
        #region Properties

        /// <summary>
        /// Repository for Modules
        /// </summary>
        public IGenericRepository<Module> ModuleRepository { get; set; }

        #endregion

        #region Implementation of ISettingsBll

        /// <summary>
        ///     Query for all main modules to edit their settings.
        /// </summary>
        /// <returns>All modules with type main o</returns>
        public IEnumerable<Module> AllVisibleMainModules()
        {
            var modules = ModuleRepository.GetAll()
                                          .Where( module => module.Type == ModuleType.Main )
                                          .ToList();

            return modules;
        }

        /// <summary>
        ///     Get the Module with the given id
        /// </summary>
        /// <param name="moduleId">the id of the module</param>
        /// <returns>the module with the given id</returns>
        public Module GetModule( Int32 moduleId )
            => ModuleRepository.FindAsync( moduleId )
                               .Result;

        /// <summary>
        ///     Updates the visabilty of the Module in the db
        /// </summary>
        /// <param name="id">the id of the module</param>
        /// <param name="visability">the visability of the module</param>
        public Module UpdateModuleVisability( Int32 id, Boolean visability )
        {
            var module = GetModule( id );
            module.Visible = visability;
            ModuleRepository.SaveChanges();
            return module;
        }

        #endregion
    }
}