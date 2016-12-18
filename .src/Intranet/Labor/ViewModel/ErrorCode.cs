#region Usings

using System;

#endregion

namespace Intranet.Labor.ViewModel
{
    /// <summary>
    ///     Class representing an formated ErrorCode for the viewmodel
    /// </summary>
    public class ErrorCode
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the ID of the Error
        /// </summary>
        public Int32 ErrorId { get; set; }

        /// <summary>
        ///     Gets or sets the Name of the Error
        /// </summary>
        public String Name { get; set; }

        #endregion
    }
}