using System;
using Intranet.Labor.Model;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model of the penetration time
    /// </summary>
    public class BabyDiaperPenetrationTime
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the first addition
        /// </summary>
        /// <value>penetration time value for the first addition</value>
        public Double PenetrationTimeAdditionFirst { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the second addition
        /// </summary>
        /// <value>penetration time value for the second addition</value>
        public Double PenetrationTimeAdditionSecond { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the third addition
        /// </summary>
        /// <value>penetration time value for the third addition</value>
        public Double PenetrationTimeAdditionThird { get; set; }

        /// <summary>
        ///     Gets or sets the value of the penetrationTime of the fourth addition
        /// </summary>
        /// <value>penetration time value for the fourth addition</value>
        public Double PenetrationTimeAdditionFourth { get; set; }

        /// <summary>
        ///     Gets or sets the rw type for the the penetrationTime of the fourth addition
        /// </summary>
        /// <value>the rw type for the penetrationTime of the fourth addition</value>
        public RwType PenetrationTimeAdditionFourthRwType { get; set; }

        #endregion
    }
}