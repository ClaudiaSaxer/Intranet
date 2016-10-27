using System;

namespace Intranet.Labor.Model.fa
{
    /// <summary>
    ///     The Class representing the ProductionOrder
    /// </summary>
    public class ProductionOrder
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the ProductionOrder
        /// </summary>
        /// <value>the id of the ProductionOrder</value>
        public Int32 FaId { get; set; }

        /// <summary>
        ///     Gets or sets the number of the ProductionOrder
        /// </summary>
        /// <value>the number of the ProductionOrder</value>
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the start Date and Time of the ProductionOrder
        /// </summary>
        /// <value>The start date and time of the ProductionOrder</value>
        public DateTime Start { get; set; }

        /// <summary>
        ///     Gets or sets the end Data and Time of the ProductionOrder
        /// </summary>
        /// <value>The end date and time of the ProductionOrder</value>
        public DateTime End { get; set; }

        #endregion
    }
}