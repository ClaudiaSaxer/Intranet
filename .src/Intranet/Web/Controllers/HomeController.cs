using System.Web.Mvc;
using Autofac;
using Intranet.Definition;
using Intranet.ViewModel;
using Intranet.Web.IoC;

namespace Intranet.Web.Controllers
{
    public class HomeController : Controller
    {
     
        #region Properties

        public IVMHelper VmHelper { get; set; }

        
        #endregion

        public HomeController( IVMHelper helper )
        {
            VmHelper = helper;
        }
        #region Ctor

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