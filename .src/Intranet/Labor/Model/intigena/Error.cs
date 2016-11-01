using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Intranet.Labor.Model.labor;

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

        /// <summary>
        ///     Gets or sets the test notes existing with this error
        /// </summary>
        /// <value>the test notes with this error</value>
        public ICollection<TestValueNote> TestNotes { get; set; }

        #endregion
    }
}