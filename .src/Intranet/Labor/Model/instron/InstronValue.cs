using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        #endregion
    }
}