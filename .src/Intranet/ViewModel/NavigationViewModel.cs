using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel of the navigation
    /// </summary>
    public class NavigationViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the main modules
        /// </summary>
        /// <value>the main modules from the shell</value>
        public IEnumerable<Module> MainModules { get; set; }

        /// <summary>
        ///     Gets or sets the setting modules
        /// </summary>
        /// <value>the setting modules from the shell</value>
        public IEnumerable<Module> SettingModules { get; set; }

        #endregion
    }
}