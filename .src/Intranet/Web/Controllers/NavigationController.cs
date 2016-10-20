using System;
using System.Web.Mvc;
using Intranet.Common;
using ControllerBase = Intranet.Common.ControllerBase;

namespace Intranet.Web.Controllers
{
    /// <summary>
    ///     TODO
    /// </summary>
    public class NavigationController : ControllerBase
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the viewmodel for the navigation
        /// </summary>
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="NavigationController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public NavigationController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(NavigationController) ) )
        {
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

        #region Overrides of ControllerBase

        /// <summary>Executes the request.</summary>
        protected override void ExecuteCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}