#region Usings

using System;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the BabyDiapersRetentionController
    /// </summary>
    public class BabyDiaperRetentionEditViewModel : BaseTestEditViewModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the DiaperWeight value
        /// </summary>
        /// <value>
        ///     The DiaperWeight value
        /// </value>
        public Double DiaperWeight { get; set; }

        /// <summary>
        ///     Gets or sets the WeightRetentionWet value
        /// </summary>
        /// <value>
        ///     The WeightRetentionWet value
        /// </value>
        public Double WeightRetentionWet { get; set; }

        #endregion
    }
}