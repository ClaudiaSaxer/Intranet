using System.Web.Optimization;

namespace Intranet.Web
{
    /// <summary>
    ///     THe Confic Bundle
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// <summary>
        ///     Registers script Bundles
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles( BundleCollection bundles )
        {
            bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
                             "~/resources/js/jquery-{version}.js",
                             "~/resources/js/bootstrap.min.js",
                             "~/resources/js/moment.min.js",
                            "~/resources/js/bootstrap-sortable.js") );

            bundles.Add( new ScriptBundle( "~/bundles/jqueryval" ).Include(
                             "~/resources/js/jquery.validate*" ) );

            bundles.Add( new ScriptBundle( "~/bundles/bootstrap" ).Include(
                             "~/resources/js/bootstrap.js",
                             "~/resources/js/respond.js" ) );

            bundles.Add( new StyleBundle( "~/bundles/css" ).Include(
                             "~/resources/css/bootstrap.min.css",
                             "~/resources/css/bootstrap-sortable.css",
                             "~/resources/css/bootstrap-theme.min.css",
                             "~/resources/css/bootstrap-toggle.min.css",
                             "~/resources/css/intigena/site.css" ) );
        }
    }
}