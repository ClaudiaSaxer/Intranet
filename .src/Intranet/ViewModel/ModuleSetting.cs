using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.ViewModel
{
    /// <summary>
    ///     Class representing ModuleSettings
    /// </summary>
    public abstract class ModuleSetting
    {
        /// <summary>
        ///     Gets or sets the name of the module
        /// </summary>
        /// <value>The name of the module</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets the visibility of the module
        /// </summary>
        /// <value>The visibility status of the module</value>
        public Boolean Visible { get; set; }
    }
}
