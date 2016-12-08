using System;
using Intranet.Labor.Model;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the dashboard information
    /// </summary>
    public class DashboardInfo
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the key of the information
        /// </summary>
        /// <value>the information key</value>
        public String InfoKey { get; set; }

        /// <summary>
        ///     Gets or sets the value of the information
        /// </summary>
        /// <value>the information value</value>
        public String InfoValue { get; set; }

        /// <summary>
        ///     Gets or sets the rw type
        /// </summary>
        /// <value>the rw type</value>
        public RwType RwType { get; set; }

        #endregion
    }
}