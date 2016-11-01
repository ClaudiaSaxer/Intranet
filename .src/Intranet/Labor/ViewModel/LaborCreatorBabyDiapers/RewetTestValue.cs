﻿namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model of the rewet test value
    /// </summary>
    public class RewetTestValue
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the rewet data
        /// </summary>
        /// <value>the rewet data</value>
        public Rewet Rewet { get; set; }

        /// <summary>
        ///     Gets or sets the test info
        /// </summary>
        /// <value>the test info</value>
        public TestInfo TestInfo { get; set; }

        #endregion
    }
}