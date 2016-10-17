using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.ViewModel
{
    /// <summary>
    ///     Class representing a SettingsViewModel
    /// </summary>
    public class SettingsViewModel
    {
        /// <summary>
        ///     Gets or sets the ModuleSettings for the SettingsViewModel
        /// </summary>
        /// <value>
        ///     The ModuleSettings
        /// </value>
        public IEnumerable<ModuleSetting> ModuleSettings { get; set; }
    }
}
