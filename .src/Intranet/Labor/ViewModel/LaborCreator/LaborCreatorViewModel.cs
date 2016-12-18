#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing View Model for Labor Creator
    /// </summary>
    public class LaborCreatorViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets a collection of running produciton orders
        /// </summary>
        /// <value>running production orders</value>
        public ICollection<RunningProductionOrder> ProductionOrders { get; set; }

        /// <summary>
        ///     Gets or sets the chosen Production Order
        /// </summary>
        /// <value>the chosen production order</value>
        [DisplayName( "Fertigungsnummer" )]
        [Required( ErrorMessage = "Die Fertigungsnummer ist zwingend." )]
        [DataType( DataType.Text, ErrorMessage = "Nummer muss ein Test sein" )]
        [StringLength( 1024, ErrorMessage = "Fertigungsnummer darf nicht länger als 1024 Zeichen sein." )]
        [MinLength( 3, ErrorMessage = "Fertigunsnummer muss mindestens 3 Zeichen lang sein." )]
        [RegularExpression( @"FA[0-9]*", ErrorMessage = "Fertigungsnummer muss mit FA beginnen und mit Nummern Enden." )]
        public String ChosenPo { get; set; }

        /// <summary>
        ///     Message for User Help Information, Empty if no message available
        /// </summary>
        public String Message { get; set; }

        #endregion
    }
}