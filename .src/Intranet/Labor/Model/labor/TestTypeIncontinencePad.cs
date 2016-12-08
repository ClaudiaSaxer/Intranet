namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Enum representing the types of tests for incontinence pads
    /// </summary>
    public enum TestTypeIncontinencePad
    {
        /// <summary>
        ///     A Test representing a rewet free test
        /// </summary>
        RewetFree,

        /// <summary>
        ///     A Test representing a retention test
        /// </summary>
        Retention,

        /// <summary>
        ///     A Test representing acquisition time test and a rewet after acquisition time test
        /// </summary>
        AcquisitionTimeAndRewet
    }
}