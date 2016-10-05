using System.Web.Mvc;
using Intranet.Definition;
using Intranet.ViewModel;

namespace Intranet.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Properties

        public IVMHelper VmHelper { get; set; }

        #endregion

        #region Ctor

        public HomeController( IVMHelper VmHelper )
        {
            this.VmHelper = VmHelper;
        }

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
            TestViewModel view = VmHelper.getTestVM();
            return View( view );
        }
    }
}