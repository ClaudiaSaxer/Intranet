using System.Web.Mvc;

namespace Intranet.Common
{
    /// <summary>
    ///     Abstract base class for MVC controllers.
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        #region Fields

        /// <summary>
        ///     The logger used by the controller.
        /// </summary>
        protected readonly ILogger Logger;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the viewmodel for the navigation
        /// </summary>
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ControllerBase" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        protected ControllerBase( ILogger logger )
        {
            Logger = logger;
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        // GET: Navigation
        /// <summary>
        ///     The Partial View Result for the Navigation with the MainModules
        /// </summary>
        /// <returns>Partial View Result with Navigation Modules</returns>
        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult GetNavigation()
        {
            return PartialView( "_HeaderNavBar", NavigationService.GetNavigationViewModel() );
        }
    }
}