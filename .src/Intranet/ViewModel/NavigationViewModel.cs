using System;
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

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ActionName { get; set; }

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ControllerName { get; set; }

        /// <summary>
        ///     Gets or sets the are to the module
        /// </summary>
        /// <value>The are of the module.</value>
        public String AreaName { get; set; }

        #endregion
    }
}