using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model.labor
{
    /// <summary>
    ///     Class representing notes for a test
    /// </summary>
    public class TestNote
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the test note
        /// </summary>
        /// <value>the test note id </value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 TestNoteId { get; set; }

        /// <summary>
        ///     Gets or sets the message for the test note
        /// </summary>
        /// <value>the message for the test note</value>
        public String Message { get; set; }

        /// <summary>
        ///     Gets or sets the error of the test note
        /// </summary>
        /// <value>the error of the test note</value>
        public Error Error { get; set; }

        #endregion
    }
}