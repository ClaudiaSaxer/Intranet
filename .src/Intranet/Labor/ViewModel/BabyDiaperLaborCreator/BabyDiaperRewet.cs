﻿#region Usings

using System;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model of the rewet
    /// </summary>
    public class BabyDiaperRewet
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the value for the revet after 140ml
        /// </summary>
        /// <value>the value for the revet after 140ml</value>
        public Double Rewet140Value { get; set; }

        /// <summary>
        ///     Gets or sets the value for revet after 120ml
        /// </summary>
        /// <value>the value for the revet after 120ml</value>
        public Double Rewet210Value { get; set; }

        /// <summary>
        ///     Gets or sets the value after the strike trough (with 210ml in g)
        /// </summary>
        /// <value>the value after the strike through</value>
        public Double StrikeThroughValue { get; set; }

        /// <summary>
        ///     Gets or sets the distribution after the strikte trough (in mm)
        /// </summary>
        /// <value>the distribution after the strikte trough</value>
        public Double DistributionOfTheStrikeTrough { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the revet for 140ml
        /// </summary>
        /// <value>the RW of the revet for 140ml</value>
        public RwType Rewet140Rw { get; set; }

        /// <summary>
        ///     Gets or sets the RW of the revet for 210ml
        /// </summary>
        /// <value>the RW of the revet for 210ml</value>
        public RwType Rewet210Rw { get; set; }

        #endregion
    }
}