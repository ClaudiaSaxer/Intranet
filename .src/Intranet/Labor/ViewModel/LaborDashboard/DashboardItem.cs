using System;
using System.Collections.Generic;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing a Dashboard Item
    /// </summary>
    public class DashboardItem
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the name of the machine
        /// </summary>
        /// <value>the name of the machine</value>
        public String MachineName { get; set; }

        /// <summary>
        ///     Gets or sets the shiftitems
        /// </summary>
        /// <value>the shift items</value>
        public ICollection<ShiftItem> ShiftItems { get; set; }

        #endregion
    }
}