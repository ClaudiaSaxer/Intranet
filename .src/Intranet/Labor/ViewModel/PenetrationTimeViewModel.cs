﻿using System;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    /// Class representing the view model of the penetration time
    /// </summary>
    public class PenetrationTimeViewModel
    {

        /// <summary>
        ///     Gets or sets the production code from the diaper
        /// </summary>
        /// <value>the production code from the diaper</value>
        public String ProductionCode { get; set; }

        /// <summary>
        ///     Gets or sets the weight the diaper has if it is dry
        /// </summary>
        /// <value>the weight of the dry diaper</value>
        public Double WeightyDiaperDry { get; set; }
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
    }
}