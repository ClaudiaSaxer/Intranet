using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model.labor
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
        public String Message { get; set; }

        /// <summary>
        ///     Gets or sets the error of the test note
        /// </summary>
        /// <value>the error of the test note</value>
        [ForeignKey( "ErrorRefId" )]
        public Error Error { get; set; }

        /// <summary>
        ///     Gets or sets the ref id of the error
        /// </summary>
        /// <value>the ref id of the error</value>
        public Int32 ErrorRefId { get; set; }

        /// <summary>
        ///     Gets or sets the test value for the error
        /// </summary>
        /// <value>the test value</value>
        [ForeignKey( "TestValueRefId" )]
        public TestValue TestValue { get; set; }

        /// <summary>
        ///     Gets or sets the ref id of the test value
        /// </summary>
        /// <value>the ref id of the test value</value>
        public Int32 TestValueRefId { get; set; }

        #endregion
    }
}