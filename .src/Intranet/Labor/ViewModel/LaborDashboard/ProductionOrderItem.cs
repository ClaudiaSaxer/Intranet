using System;
using System.Collections.Generic;
using Intranet.Labor.Model;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing a item for the production order
    /// </summary>
    public class ProductionOrderItem
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the production order name
        /// </summary>
        /// <value>the production order name</value>
        public String ProductionOrderName { get; set; }

        /// <summary>
        ///     Gets or sets the id of the sheet
        /// </summary>
        public Int32 SheetId { get; set; }

        /// <summary>
        ///     Gets or sets the rw type, telling if something is wrong.
        /// </summary>
        /// <value>seths the type</value>
        public RwType RwType { get; set; }

        /// <summary>
        ///     Gets or sets if the production order has existing notes
        /// </summary>
        /// <value>if the production order has existing notes</value>
        public Boolean HasNotes { get; set; }

        /// <summary>
        ///     Gets or sets a Collection of Notes
        /// </summary>
        /// <value>the notes</value>
        public ICollection<DashboardNote> Notes { get; set; }

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

        /// <summary>
        ///     Gets or sets the additional dashboard infor
        /// </summary>
        /// <value>the dashboard info</value>
        public ICollection<DashboardInfo> DashboardInfos { get; set; }

        #endregion
    }
}