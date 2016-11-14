using System;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    /// Class representing the view model of the acquisation time
    /// </summary>
    public class IncontinencePadAcquisitionTime
    {


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

    }
}