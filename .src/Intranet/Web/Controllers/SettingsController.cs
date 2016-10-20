using System.Web.Mvc;
using Intranet.Common;
using Intranet.Definition.Bll;
using Intranet.ViewModel;
using ControllerBase = Intranet.Common.ControllerBase;

namespace Intranet.Web.Controllers
{
    /// <summary>
    ///     Class representing the SettingsController
    /// </summary>
    public class SettingsController : ControllerBase
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="ISettingsService" />
        /// </summary>
        /// <value>
        ///     <see cref="ISettingsService" />
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
        {
            var viewModel = SettingsService.GetSettingsViewModel();
            return View( viewModel );
        }

        /// <summary>
        ///     Update a module setting
        /// </summary>
        /// <param name="moduleSetting">The moduleSetting which will be updated</param>
        /// <returns>Redirect to the Index View</returns>
        [HttpPost]
        public ActionResult Update( ModuleSetting moduleSetting )
        {
            try
            {
                SettingsService.UpdateModuleSetting( moduleSetting );
                return RedirectToAction( "Index" );
            }
            catch
            {
                return View();
            }
        }
    }
}