using Intranet.Model;
using Intranet.ViewModel;

namespace Intranet.Definition.Bll
{
    /// <summary>
    ///     The Interface for the SettingsController
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        ///     Gets the SettingsViewModel
        /// </summary>
        /// <returns>The SettingsViewModel</returns>
        SettingsViewModel GetSettingsViewModel();

        /// <summary>
        ///     Updates the settings for the module
        /// </summary>
        /// <param name="moduleSetting">The ModuleSettings</param>
        Module UpdateModuleSetting(ModuleSetting moduleSetting);
    }
}