using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Labor.Model;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing an history item for the history viewmodel
    /// </summary>
    public class HistoryItem
    {
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
        ///     Gets or sets the Shift
        /// </summary>
        /// <value>the shift of the test sheet</value>
        public ShiftType ShiftType { get; set; }

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
    }
}
