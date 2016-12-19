#region Usings

using System.Collections.ObjectModel;
using Extend;
using Intranet.Common;
using Intranet.Definition;
using Intranet.Model;
using Intranet.ViewModel;

#endregion

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the service for the settings
    /// </summary>
    public class SettingsService : ServiceBase, ISettingsService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the settings.
        /// </summary>
        public ISettingsBll SettingsBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="SettingsService" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public SettingsService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(SettingsService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of ISettingsService

        /// <summary>
        ///     Gets the SettingsViewModel
        /// </summary>
        /// <returns>The SettingsViewModel</returns>
        public SettingsViewModel GetSettingsViewModel()
        {
            var settings = new Collection<ModuleSetting>();

            SettingsBll.AllVisibleMainModules()
                       .ForEach( module => settings.Add(
                                     new ModuleSetting
                                     {
                                         Id = module.ModuleId,
                                         Name = module.Name,
                                         Visible = module.Visible == true
                                     } ) );

            var vm = new SettingsViewModel
            {
                ModuleSettings = settings
            };

            return vm;
        }

        /// <summary>
        ///     Updates the visibility for the module
        /// </summary>
        /// <param name="moduleSetting">The ModuleSettings</param>
        public Module UpdateModuleSetting( ModuleSetting moduleSetting )
            => SettingsBll.UpdateModuleVisability( moduleSetting.Id, moduleSetting.Visible );

        #endregion
    }
}