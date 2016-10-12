using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Intranet.Web.App_Start;
using Intranet.Web.ControllerFactory;
using Intranet.Web.ViewEngine;

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
            MefConfig.ConfigureContainer();
            BundleConfig.RegisterBundles( BundleTable.Bundles );
        }


     
    }
}