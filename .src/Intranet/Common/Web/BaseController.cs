using System.Web.Mvc;

namespace Intranet.Common
{
    /// <summary>
    ///     Abstract base class for MVC controllers.
    /// </summary>
    public abstract class BaseController : Controller
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
        ///     Initialize a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        protected BaseController( ILogger logger )
        {
            Logger = logger;
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        /// <inheritdoc />
        protected override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            ViewBag.Navigation = NavigationService.GetNavigationViewModel();
            base.OnActionExecuting( filterContext );
        }
    }
}