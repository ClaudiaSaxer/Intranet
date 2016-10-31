using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Common;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    ///     Class representing the BabyWindelnRetentionController
    /// </summary>
    public class BabyWindelnRetentionController : BaseController
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="BabyWindelnRetentionController" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public BabyWindelnRetentionController(ILoggerFactory loggerFactory)
            : base( loggerFactory.CreateLogger( typeof(BabyWindelnRetentionController) ) )
        {
            Logger.Trace("Enter Ctor - Exit.");
        }

        #endregion

        /// <summary>
        ///     Loads the BabywindelnRetetion Edit View with a new Item
        /// </summary>
        /// <returns>The Index View filled with the viewModel</returns>
        public ActionResult Create() => View("Edit");
    }
}