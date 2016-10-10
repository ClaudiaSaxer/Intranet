using System.Web.Mvc;
using System.Web.Routing;

namespace Intranet.Web
{
    /// <summary>
    ///     Route Configuration
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        ///     Registers Routes
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}