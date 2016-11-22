using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Intranet.Definition;
using Intranet.Web.Controllers;

namespace Intranet.Web.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckDisableAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public String ModuleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICheckDisableService CheckDisableService { get; set; }

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!CheckDisableService.IsVisible(ModuleName))
                filterContext.Result = new HttpNotFoundResult("Das Module " + ModuleName + " ist zurzeit nicht Verfügbar. Bitte wenden Sie sich an den Administrator.");
            /*filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                                                 new { controller = "Home", action = "Index", area = "" }));*/
        }
    }
}