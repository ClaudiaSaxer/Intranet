using System;

namespace Intranet.Labor.Model.fa
{
    /// <summary>
    ///     The Class representing the Fertigungsauftrag
    /// </summary>
    public class Fertigungsauftrag
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the Fertigungsauftrag
        /// </summary>
        /// <value>the id of the Fertigungsauftrag</value>
        public Int32 FaId { get; set; }

        /// <summary>
        ///     Gets or sets the number of the Fertigungsauftrag
        /// </summary>
        /// <value>the number of the Fertigungsauftrag</value>
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the start Date and Time of the Fertigungsauftrag
        /// </summary>
        /// <value>The start date and time of the Fertigungsauftrag</value>
        public DateTime Start { get; set; }

        /// <summary>
        ///     Gets or sets the end Data and Time of the Fertigungsauftrag
        /// </summary>
        /// <value>The end date and time of the Fertigungsauftrag</value>
        public DateTime End { get; set; }

        #endregion
    }
}