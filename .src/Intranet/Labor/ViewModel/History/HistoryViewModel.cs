#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the History ViewModel for the HistoryController + View
    /// </summary>
    public class HistoryViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the FA Number
        /// </summary>
        /// <value>
        ///     The FaNr
        /// </value>
        [DisplayName("Fertigungsnummer")]
        [DataType(DataType.Text, ErrorMessage = "Nummer muss ein Test sein")]
        [StringLength(1024, ErrorMessage = "Fertigungsnummer darf nicht länger als 1024 Zeichen sein.")]
        [MinLength(3, ErrorMessage = "Fertigunsnummer muss mindestens 3 Zeichen lang sein.")]
        [RegularExpression(@"FA[0-9]*", ErrorMessage = "Fertigungsnummer muss mit FA beginnen und mit Nummern Enden.")]
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets a Info-Message for the View
        /// </summary>
        /// <value>
        ///     The info message
        /// </value>
        public String Message { get; set; }

        /// <summary>
        ///     Gets or sets the Sheets
        ///     Collection contains an HistoryItem
        /// </summary>
        public ICollection<HistoryItem> Sheets { get; set; }

        #endregion
    }
}