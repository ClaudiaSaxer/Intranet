using System;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing the TestNote model for all test viewmodels
    /// </summary>
    public class TestNote
    {
        #region Properties

        /// <summary>
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// </summary>
        public Int32 ErrorCodeId { get; set; }

        /// <summary>
        /// </summary>
        public String Message { get; set; }

        #endregion
    }
}