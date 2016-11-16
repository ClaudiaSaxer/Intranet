namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model for the retention test value
    /// </summary>
    public class IncontinencePadRetentionTestValue
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the retention data
        /// </summary>
        /// <value>the retention data</value>
        public IncontinencePadRetention IncontinencePadRetention { get; set; }

        /// <summary>
        ///     Gets or sets the test info
        /// </summary>
        /// <value>the test info</value>
        public IncontinencePadTestInfo IncontinencePadTestInfo { get; set; }

        #endregion
    }
}