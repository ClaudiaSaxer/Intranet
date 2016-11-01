using System;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    /// Class representing the view model for the retention
    /// </summary>
    public class Retention
    {
      

        /// <summary>
        ///     Gets or sets the RW of the retention
        /// </summary>
        /// <value>the RW of the retention</value>
        public RwType RetentionRw { get; set; }

        /// <summary>
        ///     Gets or sets the rentention value after the zentrifuge
        /// </summary>
        /// <value>the retention value after zentrifuge</value>
        public Double RetentionAfterZentrifugeValue { get; set; }

        /// <summary>
        ///     Gets or sets the rentention percent after the zentrifuge
        /// </summary>
        /// <value>the retention percent after the zentrifuge</value>
        public Double RetentionAfterZentrifugePercent { get; set; }

        /// <summary>
        ///     Gets or sets the value for the SAP g/Höwi
        /// </summary>
        /// <value>the SAP g/Höwi value</value>
        public Double SapGHoewiValue { get; set; }
    }
}