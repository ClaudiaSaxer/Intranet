#region Usings

using System;
using Intranet.Labor.Model.labor;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the ViewModel for the BabyDiaperRewetController
    /// </summary>
    public class BabyDiaperRewetEditViewModel : BaseTestEditViewModel
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
        ///     Gets or sets the RewetAfter140 value
        /// </summary>
        /// <value>
        ///     The RewetAfter140 value
        /// </value>
        public Double RewetAfter140 { get; set; }

        /// <summary>
        ///     Gets or sets the RewetAfter210 value
        /// </summary>
        /// <value>
        ///     The RewetAfter210 value
        /// </value>
        public Double RewetAfter210 { get; set; }

        /// <summary>
        ///     Gets or sets the StrikeThrough value
        /// </summary>
        /// <value>
        ///     The StrikeThrough value
        /// </value>
        public Double StrikeThrough { get; set; }

        /// <summary>
        ///     Gets or sets the Distribution value
        /// </summary>
        /// <value>
        ///     The Distribution value
        /// </value>
        public Double Distribution { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime1 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime1 value
        /// </value>
        public Double PenetrationTime1 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime2 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime2 value
        /// </value>
        public Double PenetrationTime2 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime3 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime3 value
        /// </value>
        public Double PenetrationTime3 { get; set; }

        /// <summary>
        ///     Gets or sets the PenetrationTime4 value
        /// </summary>
        /// <value>
        ///     The PenetrationTime4 value
        /// </value>
        public Double PenetrationTime4 { get; set; }

        /// <summary>
        ///     Gets or sets the TestType for the babydiaper
        /// </summary>
        /// <value>
        ///     The TestType value
        /// </value>
        public TestTypeBabyDiaper TestType { get; set; }

        #endregion
    }
}