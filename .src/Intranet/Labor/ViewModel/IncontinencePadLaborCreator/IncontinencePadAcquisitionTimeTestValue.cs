namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the view model of the penetration time test value
    /// </summary>
    public class IncontinencePadAcquisitionTimeTestValue
    {
        #region Properties
        /// <summary>
        ///     Gets or sets the test info
        /// </summary>
        /// <value>the test info</value>
        public IncontinencePadTestInfo IncontinencePadTestInfo { get; set; }

        /// <summary>
        ///     Gets or sets the acquisition time data
        /// </summary>
        /// <value>the acquisition time data</value>
        public IncontinencePadAcquisitionTime AcquisitionTime { get; set; }

        #endregion
    }
}