#region Usings

using System.Web.Mvc;

#endregion

namespace Intranet.Web
{
    /// <summary>
    ///     Filter Config
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        ///     Registers Global Filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters( GlobalFilterCollection filters )
            => filters.Add( new HandleErrorAttribute() );
    }
}