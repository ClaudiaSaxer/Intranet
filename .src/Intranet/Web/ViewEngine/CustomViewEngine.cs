using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Intranet.Web.ViewEngine
{
    /// <summary>
    ///     View Engine which locates the location of the views
    /// </summary>
    public class CustomViewEngine : RazorViewEngine
    {
        #region Fields

        private readonly List<String> _plugins;

        #endregion

        #region Ctor

        /// <summary>
        ///     Inizialize the CustomViewEngine
        /// </summary>
        /// <param name="pluginPaths">Paths where the Controller.dll from the modules are stored</param>
        public CustomViewEngine( IEnumerable<String> pluginPaths )
        {
            _plugins = GetModuleNames(pluginPaths);

            ViewLocationFormats = GetViewLocations();
            MasterLocationFormats = GetMasterLocations();
            PartialViewLocationFormats = GetViewLocations();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the paths to all Views
        /// </summary>
        /// <returns></returns>
        private string[] GetViewLocations()
        {
            var views = new List<string>();
            views.Add("~/Views/{1}/{0}.cshtml");

            _plugins.ForEach(plugin =>
                views.Add("~/Areas/" + plugin + "/Views/{1}/{0}.cshtml")
            );
            return views.ToArray();
        }

        /// <summary>
        ///     Gets the paths to all Shared Views
        /// </summary>
        /// <returns></returns>
        private string[] GetMasterLocations()
        {
            var masterPages = new List<String>();

            masterPages.Add("~/Views/Shared/{0}.cshtml");

            _plugins.ForEach(plugin =>
                masterPages.Add("^/Areas/" + plugin + "/Views/Shared/{0}.cshtml")
            );

            return masterPages.ToArray();
        }

        private List<String> GetModuleNames( IEnumerable<String> pluginPath )
        {
            var result = new List<String>();
            foreach ( var directory in pluginPath.Select( d => new DirectoryInfo(d) ) )
                result.AddRange(directory.GetFiles("*.Controllers.dll")
                                         .Select(f => f.Name.Split('.')[1])
                                         .ToList() );

            return result;
        }

        #endregion

    }
}