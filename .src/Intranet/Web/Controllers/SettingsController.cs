#region Usings

using System.Web.Mvc;
using Intranet.Common;
using Intranet.Definition;
using Intranet.ViewModel;

#endregion

namespace Intranet.Web.Controllers
{
    /// <summary>
    ///     Class representing the SettingsController
    /// </summary>
    [Authorize( Roles = RoleSettings.LaborAdmin )]
    public class SettingsController : BaseController
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IHomeBll" />
        /// </summary>
        /// <value>
        ///     <see cref="IHomeBll" />
        /// </value>
        public ISettingsService SettingsService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public SettingsController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(SettingsController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <summary>
        ///     Loads the index page of the SettingsController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Index()
            => View( "Index", SettingsService.GetSettingsViewModel() );

        /// <summary>
        ///     Update a module setting
        /// </summary>
        /// <param name="moduleSetting">The moduleSetting which will be updated</param>
        /// <returns>Redirect to the Index View</returns>
        [HttpPost]
        public ActionResult Update( ModuleSetting moduleSetting )
        {
            if ( SettingsService.UpdateModuleSetting( moduleSetting ) != null )
                return RedirectToAction( "Index" );
            Logger.Error( "Module Settings haven't been updated!" );
            return HttpNotFound();
        }
    }
}