using System;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the running Production Orders
    /// </summary>
    public class RunningProductionOrder
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the production id
        /// </summary>
        /// <value>the production order id</value>
        public Int32 PoId { get; set; }

        /// <summary>
        ///     Gets or sets the production order name
        /// </summary>
        public String PoName { get; set; }

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ActionName { get; set; }

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ControllerName { get; set; }

        /// <summary>
        ///     Gets or sets the area to the module
        /// </summary>
        /// <value>The area of the module.</value>
        public String AreaName { get; set; }

        /// <summary>
        ///     Gets or sets the description to the module
        /// </summary>
        /// <value>The description of the module.</value>
        public String Description { get; set; }

        #endregion
    }
}