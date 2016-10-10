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
            var pluginPaths = GetPluginPaths();

            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            MefConfig.ConfigureContainer( pluginPaths );
            BundleConfig.RegisterBundles( BundleTable.Bundles );
            ControllerBuilder.Current.SetControllerFactory( new CustomControllerFactory() );
            ViewEngines.Engines.Add( new CustomViewEngine( pluginPaths ) );
        }


        private List<String> GetPluginPaths()
        {
            var result = new List<String>();

            var directory = AppDomain.CurrentDomain.BaseDirectory.Replace("\\Web", "");
            var controllerDirectories = Directory.GetDirectories(directory, "Controllers", SearchOption.AllDirectories).ToList();
            var binDirectories = new List<String>();
            foreach ( var d in controllerDirectories )
                binDirectories.AddRange(Directory.GetDirectories(d, "bin", SearchOption.AllDirectories).ToList());
            foreach (var d in binDirectories)
                result.AddRange(Directory.GetDirectories(d, "Debug", SearchOption.AllDirectories).ToList());

            return result;
        }
    }
}