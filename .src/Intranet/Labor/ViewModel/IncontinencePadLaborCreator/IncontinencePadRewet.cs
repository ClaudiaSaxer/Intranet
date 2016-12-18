#region Usings

using System;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model of the rewet
    /// </summary>
    public class IncontinencePadRewet
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the wet weight value
        /// </summary>
        /// <value>the wet weight value </value>
        public Double WeightWet { get; set; }

        /// <summary>
        ///     Gets or sets the weight the diaper has if it is dry
        /// </summary>
        /// <value>the weight of the dry diaper</value>
        public Double WeightDry { get; set; }

        /// <summary>
        ///     Gets or sets the weight diff value
        /// </summary>
        /// <value>the weight diff value </value>
        public Double WeightDiff { get; set; }

        /// <summary>
        ///     Gets or sets the RW type
        /// </summary>
        /// <value>the RW type</value>
        public RwType RewetRW { get; set; }

        #endregion
    }
}