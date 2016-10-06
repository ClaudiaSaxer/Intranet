using System.Web;
using System.Web.Mvc;

namespace Intranet.Web
{
    /// <summary>
    /// Filter Config
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers Global Filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) 
            => filters.Add(new HandleErrorAttribute());
    }
}
