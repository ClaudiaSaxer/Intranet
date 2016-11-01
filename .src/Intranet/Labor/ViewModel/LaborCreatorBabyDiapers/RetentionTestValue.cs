using System;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    /// Class representing the view model for the retention test value
    /// </summary>
    public class RetentionTestValue
    {
        /// <summary>
        ///     Gets or sets the retention data
        /// </summary>
        /// <value>the retention data</value>
        public Retention Retention { get; set; }

        /// <summary>
        ///     Gets or sets the test info
        /// </summary>
        /// <value>the test info</value>
        public TestInfo TestInfo { get; set; }

       
    }
}