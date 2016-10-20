using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the HomeController
    /// </summary>
    public class HomeViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Modules for the HomeViewModel
        /// </summary>
        /// <value>
        ///     The Modules
        /// </value>
        public IEnumerable<Module> Modules { get; set; }

        #endregion
    }
}