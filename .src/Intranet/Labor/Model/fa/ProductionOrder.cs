using System;

namespace Intranet.Labor.Model.fa
{
    /// <summary>
    ///     The Class representing the production order
    /// </summary>
    public class ProductionOrder
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the production order
        /// </summary>
        /// <value>the id of the production order</value>
        public Int32 FaId { get; set; }

        /// <summary>
        ///     Gets or sets the number of the production order
        /// </summary>
        /// <value>the number of the production order</value>
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the start Date and Time of the production order
        /// </summary>
        /// <value>The start date and time of the production order</value>
        public DateTime Start { get; set; }

        /// <summary>
        ///     Gets or sets the end Data and Time of the production order
        /// </summary>
        /// <value>The end date and time of the production order</value>
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the article for this production order <see cref="Article"/>
        /// </summary>
        /// <value>the article for the production order</value>
        public Article Article { get; set; }

        #endregion
    }
}