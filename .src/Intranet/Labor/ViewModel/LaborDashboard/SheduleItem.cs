using System.Collections.Generic;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing a Item for a Shift
    /// </summary>
    public class ShiftItem
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Production Order Items
        /// </summary>
        public ICollection<ProductionOrderItem> ProductionOrderItems { get; set; }

        #endregion
    }
}