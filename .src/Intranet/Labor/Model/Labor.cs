using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Model for Labor
    /// </summary>
    public class Labor
    {
        #region Properties

        /// <summary>
        ///     Gets and sets the Id of the Labor
        /// </summary>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Guid LaborId { get; set; }

        /// <summary>
        ///     Gets and sets the String Bla
        /// </summary>
        public String Bla { get; set; }

        #endregion
    }
}