using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Model
{
    /// <summary>
    ///     Test Model
    /// </summary>
    public class Test
    {
        #region Properties

        /// <summary>
        ///     Gets and sets the Test Id
        /// </summary>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Guid TestId { get; set; }

        /// <summary>
        ///     Gets and Sets the Test String
        /// </summary>
        public String TestString { get; set; }

        #endregion
    }
}