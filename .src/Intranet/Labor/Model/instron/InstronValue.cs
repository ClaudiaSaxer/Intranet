#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class Representing Values for Instron
    /// </summary>
    public class InstronValue
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Id for the Values of Instron
        /// </summary>
        /// <value>The id of the Instron value</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 InstronValueId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the instron value
        /// </summary>
        /// <value>The name of the instron value</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets the average of the instron value
        /// </summary>
        /// <value>The average of the instron value</value>
        public Double Average { get; set; }

        /// <summary>
        ///     Gets or sets the standard deviation of the instron value
        /// </summary>
        /// <value>the standard deviation of the instron value</value>
        public Double StandardDeviation { get; set; }

        /// <summary>
        ///     Gets or sets the Instron for the Value.
        /// </summary>
        /// <value>the instron for the value</value>
        public virtual Instron Instron { get; set; }

        /// <summary>
        ///     Gets or sets the Instron ref id
        /// </summary>
        /// <value>the instron ref id </value>
        [ForeignKey("Instron")]
        [Index("IX_InstronValue_InstronId")]
        public Int32 IntstronId { get; set; }

        #endregion
    }
}