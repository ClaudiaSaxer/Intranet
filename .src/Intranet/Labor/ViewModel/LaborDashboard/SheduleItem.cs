using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Labor.ViewModel.LaborDashboard
{
    /// <summary>
    /// Class representing a Item for a Shift
    /// </summary>
    public class ShiftItem
    {
        /// <summary>
        /// Gets or sets the Production Order Items
        /// </summary>
        public ICollection<ProductionOrderItem> ProductionOrderItems { get; set; }
    }
}
