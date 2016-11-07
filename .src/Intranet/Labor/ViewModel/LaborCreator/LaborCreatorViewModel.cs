using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Labor.ViewModel.LaborCreator;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    /// Class representing View Model for Labor Creator
    /// </summary>
    public class LaborCreatorViewModel
    {
        /// <summary>
        /// Gets or sets a collection of running produciton orders
        /// </summary>
        /// <value>running production orders</value>
        public ICollection<RunningProductionOrder> ProductionOrders { get; set; }

        /// <summary>
        /// Gets or sets the chosen Production Order
        /// </summary>
        /// <value>the chosen production order</value>
        public String ChosenPo { get; set; }
      
    }
}
