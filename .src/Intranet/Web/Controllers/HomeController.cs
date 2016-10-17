using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Intranet.Definition;
using ControllerBase = Intranet.Definition.ControllerBase;

namespace Intranet.Web.Controllers
{
    /// <summary>
    ///     Class representing the HomeController
    /// </summary>
    public class HomeController : ControllerBase
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a <see cref="IHomeService" />
        /// </summary>
        /// <value>
        ///     <see cref="IHomeService" />
        /// </value>
        public IHomeService HomeService { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public HomeController( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(HomeController) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion


        /// <summary>
        ///     Loads the index page of the HomeController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Index()
        {
            var viewModel = HomeService.GetHomeViewModel();
            return View( viewModel );
        }

        #region Overrides of ControllerBase

        #endregion
    }
}