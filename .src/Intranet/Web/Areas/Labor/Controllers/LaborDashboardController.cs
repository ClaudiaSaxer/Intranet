using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Common;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    /// Class representing a Controller for the Labor Dashboard
    /// </summary>
    public class LaborDashboardController : BaseController
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="LaborDashboardController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborDashboardController(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger(typeof(LaborDashboardController)))
        {
            Logger.Trace("Enter Ctor - Exit.");
        }


        /// <summary>
        ///     Gets or sets a <see cref="ILaborDashboardService" />
        /// </summary>
        /// <value>
        ///     <see cref="ILaborDashboardService" />
        /// </value>
        public ILaborDashboardService LaborDashboardService { get; set; }

        /// <summary>
        ///     Loads the index page of the LaborDashboardController
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Index() => View("Index", LaborDashboardService.GetLaborDashboardViewModel());
    }
}