using System;
using System.Collections.Generic;
using System.ComponentModel;
using Intranet.Labor.ViewModel.LaborCreator;
using System.ComponentModel.DataAnnotations;
using Microsoft.JScript;

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
        [DisplayName("Fertigungsnummer")]
        [Required(ErrorMessage = "Die Fertigungsnummer ist zwingend.")]
        [DataType(DataType.Text)]
        [StringLength(1024)]
        [MinLength(3)]
        [RegularExpression(@"Fa[0-9]*")]
        public String ChosenPo { get; set; }
      
    }
}
