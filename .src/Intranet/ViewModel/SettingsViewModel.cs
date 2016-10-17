using System.Collections.Generic;

namespace Intranet.ViewModel
{
    /// <summary>
    ///     Class representing a SettingsViewModel
    /// </summary>
    public class SettingsViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ModuleSettings for the SettingsViewModel
        /// </summary>
        /// <value>
        ///     The ModuleSettings
        /// </value>
        public IEnumerable<ModuleSetting> ModuleSettings { get; set; }

        #endregion
    }
}