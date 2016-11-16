using System;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.ViewModel.LaborDashboard
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