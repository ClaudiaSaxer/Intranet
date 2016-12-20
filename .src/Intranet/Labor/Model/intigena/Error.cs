#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

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
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 ErrorId { get; set; }

        /// <summary>
        ///     Gets or sets the value of the error Code
        /// </summary>
        /// <value>the value of the error code</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String ErrorCode { get; set; }

        /// <summary>
        ///     Gets or sets the value of the error
        /// </summary>
        /// <value>the value or a description of the error</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String Value { get; set; }

        /// <summary>
        ///     Gets or sets the test notes existing with this error
        /// </summary>
        /// <value>the test notes with this error</value>
        public ICollection<TestValueNote> TestNotes { get; set; }

        #endregion
    }
}