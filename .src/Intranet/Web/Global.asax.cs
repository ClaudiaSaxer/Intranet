using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Intranet.Web.IoC;

namespace Intranet.Web
{
    /// <summary>
    ///     The Application
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        ///     What to run at application start
        /// </summary>
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            BundleConfig.RegisterBundles( BundleTable.Bundles );
        }
    }
}