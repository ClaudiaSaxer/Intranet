using System.Collections.Generic;
using Intranet.Model;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the LaborHomeController
    /// </summary>
    public class LaborHomeViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Modules for the LaborHomeViewModel
        /// </summary>
        /// <value>
        ///     The Modules
        /// </value>
        public IEnumerable<Module> Modules { get; set; }

        #endregion
    }
}