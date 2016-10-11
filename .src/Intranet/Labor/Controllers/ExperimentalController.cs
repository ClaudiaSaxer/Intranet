using System.ComponentModel.Composition;
using System.Web.Mvc;

namespace Intranet.Labor.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Export("Labor.Experimental", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExperimentalController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "This is an Experimental Test";
            return View();
        }
    }
}
