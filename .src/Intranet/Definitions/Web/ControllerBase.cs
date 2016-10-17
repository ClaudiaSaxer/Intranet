using System;
using System.Web.Mvc;
using Intranet.Definition.Bll;
using Intranet.ViewModel;

namespace Intranet.Definition
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

        /// <summary>
        ///     The Partial View Result for the Navigation with the MainModules
        /// </summary>
        /// <returns>Partial View Result with Navigation Modules</returns>
        public PartialViewResult Navigation() => PartialView( "_HeaderNavBar", NavigationService.GetNavicationViewModel());

        /// <summary>
    ///     Begins to invoke the action in the current controller context.
    /// </summary>
    /// <returns>
    ///     Returns an IAsyncController instance.
    /// </returns>
    /// <param name="callback">The callback.</param>
    /// <param name="state">The state.</param>
    protected override IAsyncResult BeginExecuteCore( AsyncCallback callback, Object state )
            => base.BeginExecuteCore( callback, state );
    }
}