using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Common;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    /// Class representing Labor Creator
    /// </summary>
    public class LaborCreatorController : BaseController
    {

        /// <summary>
        /// GET: Labor/LaborCreator
        /// </summary>
        /// <returns>Action result with index</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborCreatorController( ILoggerFactory loggerFactory )
              : base(loggerFactory.CreateLogger(typeof(LaborCreatorController)))
        {


        }
    }
}