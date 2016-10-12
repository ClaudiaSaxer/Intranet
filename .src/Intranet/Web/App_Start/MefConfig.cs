#region Usings

using System;
using System.Collections.Generic;
using Intranet.Web.IoC;

#endregion

namespace Intranet.Web.App_Start
{
    /// <summary>
    ///     Class representing the configuration for Mef
    /// </summary>
    public static class MefConfig
    {
        /// <summary>
        ///     Configurate the container
        /// </summary>
        /// <param name="pluginPaths">String with pathes for the Plugins</param>
        public static void ConfigureContainer( IEnumerable<String> pluginPaths )
            => Bootstrapper.Compose( pluginPaths );
    }
}