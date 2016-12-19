#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing notes for a test value
    /// </summary>
    public class TestValueNote
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the test note
        /// </summary>
        /// <value>the test note id </value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 TestValueNoteId { get; set; }

        /// <summary>
        ///     Gets or sets the message for the test note
        /// </summary>
        /// <value>the message for the test note</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String Message { get; set; }

        /// <summary>
        ///     Gets or sets the error of the test note
        /// </summary>
        /// <value>the error of the test note</value>
        public virtual Error Error { get; set; }

        /// <summary>
        ///     Gets or sets the ref id of the error
        /// </summary>
        /// <value>the ref id of the error</value>
        [ForeignKey( "Error" )]
        [Index( "IX_TestValueNote_ErrorId" )]
        public Int32 ErrorId { get; set; }

        /// <summary>
        ///     Gets or sets the test value for the error
        /// </summary>
        /// <value>the test value</value>
        public virtual TestValue TestValue { get; set; }

        /// <summary>
        ///     Gets or sets the ref id of the test value
        /// </summary>
        /// <value>the ref id of the test value</value>
        [ForeignKey( "TestValue" )]
        [Index( "IX_TestValueNote_TestValueId" )]
        public Int32 TestValueId { get; set; }

        #endregion
    }
}