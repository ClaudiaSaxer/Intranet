using System;
using System.Collections.Generic;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the test info for the labor creator
    /// </summary>
    public class TestInfo
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
    }
}