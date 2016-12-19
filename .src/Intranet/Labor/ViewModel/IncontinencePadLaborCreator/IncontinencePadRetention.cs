#region Usings

using System;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model for the retention
    /// </summary>
    public class IncontinencePadRetention
    {
        #region Properties

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
        ///     Gets or sets the value for retention difference
        /// </summary>
        /// <value>the retention difference value</value>
        public Double RetentionDiff { get; set; }

        /// <summary>
        ///     Gets or sets the value for absorption difference
        /// </summary>
        /// <value>the absorption difference value</value>
        public Double AbsorptionDiff { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the diaper, wet after the retention
        /// </summary>
        /// <value>the weight of a wet diaper after retention</value>
        public Double RetentionWetWeight { get; set; }

        /// <summary>
        ///     Gets or sets the weight the diaper has if it is dry
        /// </summary>
        /// <value>the weight of the dry diaper</value>
        public Double RetentionDryWeight { get; set; }

        #endregion
    }
}