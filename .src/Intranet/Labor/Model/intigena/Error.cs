using System;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing Errors
    /// </summary>
    public class Error
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the error
        /// </summary>
        /// <value>the id of the error</value>
        [Key]
        public Int32 ErrorId { get; set; }

        /// <summary>
        ///     Gets or sets the value of the error
        /// </summary>
        /// <value>the value or a description of the error</value>
        public String Value { get; set; }

        #endregion
    }
}