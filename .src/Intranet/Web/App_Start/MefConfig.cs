#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Intranet.Web.ControllerFactory;
using Intranet.Web.IoC;
using Intranet.Web.ViewEngine;

#endregion

namespace Intranet.Web.App_Start
{
    /// <summary>
    ///     Class representing the configuration for Mef
    /// </summary>
    public class MefConfig
    {
        /// <summary>
        ///     Configurate the container
        /// </summary>
        public static void ConfigureContainer()
        {
            var pluginPaths = GetPluginPaths();
            Bootstrapper.Compose(pluginPaths);
            ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
            ViewEngines.Engines.Add(new CustomViewEngine(pluginPaths));
        }
           

        private static ICollection<String> GetPluginPaths()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory.Replace( "\\Web", "" );

            return Directory.EnumerateDirectories( directory )
                            .Where( d => d.ToLower()
                                          .Equals( "controllers" ) || d.ToLower()
                                                                       .Equals( "bin" ) || d.ToLower()
                                                                                            .Equals( "debug" ) )
                            .ToList();
        }
    }

  
}