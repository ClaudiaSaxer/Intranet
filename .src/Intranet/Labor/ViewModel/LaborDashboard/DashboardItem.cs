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
        ///     Gets or sets the production order items current
        /// </summary>
        /// <value>the production order items</value>
        public ICollection<ProductionOrderItem> ShiftItemsCurrent { get; set; }
        /// <summary>
        ///     Gets or sets the production order items minus 1
        /// </summary>
        /// <value>the production order items</value>
        public ICollection<ProductionOrderItem> ShiftItemsMinus1 { get; set; }
        /// <summary>
        ///     Gets or sets the production order items minus 2
        /// </summary>
        /// <value>the production order items</value>
        public ICollection<ProductionOrderItem> ShiftItemsMinus2 { get; set; }
        /// <summary>
        ///     Gets or sets the production order items minus 3
        /// </summary>
        /// <value>the production order items</value>
        public ICollection<ProductionOrderItem> ShiftItemsMinus3 { get; set; }

        #endregion
    }
}