using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Web.IoC;

namespace Intranet.Web.App_Start
{
    /// <summary>
    /// </summary>
    public class MefConfig
    {
        /// <summary>
        /// </summary>
        public static void ConfigureContainer(List<String> pluginPaths) 
            => Bootstrapper.Compose( pluginPaths );
    }
}