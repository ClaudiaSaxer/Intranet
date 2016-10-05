using System.Web.Mvc;
using Intranet.Definition;

namespace Intranet.WebTemp.Controllers
{
    public class HomeController : Controller
    {
        #region Properties

        public IVMHelper VmHelper { get; set; }

        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index()
        {
            var view = VmHelper.getTestVM();
            return View( view );
        }
    }
}