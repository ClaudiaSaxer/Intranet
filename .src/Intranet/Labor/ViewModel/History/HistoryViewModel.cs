#region Usings

using System;
using System.Collections.Generic;

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