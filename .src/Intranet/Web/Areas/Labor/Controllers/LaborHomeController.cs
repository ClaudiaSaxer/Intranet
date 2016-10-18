using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Definition;
using Intranet.Web.Controllers;
using ControllerBase = Intranet.Definition.ControllerBase;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LaborHomeController : ControllerBase
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LaborHomeController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborHomeController(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(LaborHomeController) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        // GET: Labor/LaborHome
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}