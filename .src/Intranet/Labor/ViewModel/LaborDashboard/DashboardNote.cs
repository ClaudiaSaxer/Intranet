using System;

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing a Note for the Dashboard
    /// </summary>
    public class DashboardNote
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the message for the note
        /// </summary>
        /// <value>the message</value>
        public String Message { get; set; }

        /// <summary>
        ///     Gets or sets the error code of the note
        /// </summary>
        /// <value>the defined error code</value>
        public String Code { get; set; }

        /// <summary>
        ///     Gets or sets the error message for the code
        /// </summary>
        /// <value>the error message</value>
        public String ErrorMessage { get; set; }

        #endregion
    }
}