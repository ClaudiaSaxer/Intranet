#region Usings

using System;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing an history item for the history viewmodel
    /// </summary>
    public class HistoryItem
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ID of the Test Sheet
        /// </summary>
        /// <value>
        ///     The ID of the Test Sheet
        /// </value>
        public Int32 TestSheetId { get; set; }

        /// <summary>
        ///     Gets or sets the FaNr of the Test Sheet
        /// </summary>
        /// <value>
        ///     The FaNr of the Test Sheet
        /// </value>
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the shift
        /// </summary>
        /// <value>the shift</value>
        public String Shift { get; set; }

        /// <summary>
        ///     Gets or sets the number of the machine
        /// </summary>
        /// <value>the number of the machine</value>
        public String MachineNr { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the creation of the test sheet
        /// </summary>
        /// <value>the timestamp of the creation</value>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the rw type, telling if something is wrong.
        /// </summary>
        /// <value>seths the type</value>
        public RwType RwType { get; set; }

        /// <summary>
        ///     Gets or sets the controller for more details
        /// </summary>
        /// <value>the controller for more detail</value>
        public String Controller { get; set; }

        /// <summary>
        ///     Gets or sets the action for more detail
        /// </summary>
        /// <value>the action for more detail</value>
        public String Action { get; set; }

        #endregion
    }
}