using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Model;

namespace Intranet.ViewModel
{
   /// <summary>
   /// Class representing the ViewModel of the navigation
   /// </summary>
   public class NavigationViewModel
    {
        /// <summary>
        /// Gets or sets the modules
        /// </summary>
        /// <value>the modules from the shell</value>
        public IEnumerable<Module> Modules { get; set; }
    }
}
