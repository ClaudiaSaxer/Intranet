using System.Web.Mvc;

namespace Intranet.Web.Areas.Labor
{
    public class LaborAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Labor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Labor_default",
                "Labor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}