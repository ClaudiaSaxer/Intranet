using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Intranet.Definition;
using ControllerBase = Intranet.Definition.ControllerBase;
using System.Security.Principal;

namespace Intranet.Web.Controllers
{
    /// <summary>
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
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var roleNames = Roles.GetRolesForUser();
            var view = HomeService.GetTestViewModel();
            return View( view );
        }

        #region Overrides of ControllerBase

        #endregion
    }
}