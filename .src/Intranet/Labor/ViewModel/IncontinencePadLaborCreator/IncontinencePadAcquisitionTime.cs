﻿#region Usings

using System;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model of the acquisation time
    /// </summary>
    public class IncontinencePadAcquisitionTime
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the value of the acquisition time of the first addition
        /// </summary>
        /// <value>acquisition time value for the first addition</value>
        public Double AcquisitionTimeAdditionFirst { get; set; }

        /// <summary>
        ///     Gets or sets the value of the acquisition time of the second addition
        /// </summary>
        /// <value>acquisition time value for the second addition</value>
        public Double AcquisitionTimeAdditionSecond { get; set; }

        /// <summary>
        ///     Gets or sets the value of the acquisition time of the third addition
        /// </summary>
        /// <value>acquisition time value for the third addition</value>
        public Double AcquisitionTimeAdditionThird { get; set; }

        /// <summary>
        ///     Gets or sets the RW type
        /// </summary>
        /// <value>the RW type</value>
        // ReSharper disable once InconsistentNaming
        public RwType AcquisitionTimeAdditionFirstRW { get; set; }

        /// <summary>
        ///     Gets or sets the RW type
        /// </summary>
        /// <value>the RW type</value>
        // ReSharper disable once InconsistentNaming
        public RwType AcquisitionTimeAdditionSecondRW { get; set; }

        /// <summary>
        ///     Gets or sets the RW type
        /// </summary>
        /// <value>the RW type</value>
        // ReSharper disable once InconsistentNaming
        public RwType AcquisitionTimeAdditionThirdRW { get; set; }

        /// <summary>
        ///     Gets or sets the weight the diaper has if it is dry
        /// </summary>
        /// <value>the weight of the dry diaper</value>
        public Double Weight { get; set; }

        #endregion
    }
}