using System.Web.Mvc;
using Intranet.Common;

namespace Intranet.Web.Areas.Labor.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LaborHomeController : BaseController
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