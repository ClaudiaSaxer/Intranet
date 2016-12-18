#region Usings

using System;
using System.Web.Mvc;
using Intranet.Definition;

#endregion

namespace Intranet.Web.Filter
{
    /// <summary>
    /// </summary>
    public class CheckDisableAttribute : ActionFilterAttribute
    {
        #region Properties

        /// <summary>
        /// </summary>
        public String ModuleName { get; set; }

        /// <summary>
        /// </summary>
        public ICheckDisableService CheckDisableService { get; set; }

        #endregion

        /// <inheritdoc />
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            if ( !CheckDisableService.IsVisible( ModuleName ) )
                filterContext.Result = new HttpNotFoundResult( "Das Module " + ModuleName + " ist zurzeit nicht Verfügbar. Bitte wenden Sie sich an den Administrator." );
        }
    }
}