using System;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    /// Class representing the view model of the penetration time test value
    /// </summary>
    public class PenetrationTimeTestValue
    {
        /// <summary>
        /// Gets or sets the test info
        /// </summary>
        /// <value>the test info</value>
        public TestInfo TestInfo { get; set; }

        /// <summary>
        /// Gets or sets the penetration time data
        /// </summary>
        /// <value>the penetration time data</value>
        public PenetrationTime PenetrationTime { get; set; }
    }
}