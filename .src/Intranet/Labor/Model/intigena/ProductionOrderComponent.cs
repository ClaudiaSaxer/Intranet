#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing a component
    /// </summary>
    public class ProductionOrderComponent
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the production order ref id
        /// </summary>
        [ForeignKey( "ProductionOrder" )]
        [Key]
        public Int32 ProductionOrderComponentId { get; set; }

        /// <summary>
        ///     Gets or sets the SAP
        /// </summary>
        /// <value>the SAP value of the component</value>
        public Double SAP { get; set; }

        /// <summary>
        ///     Gets or sets the value of Pillow Retent without SAP of the component
        /// </summary>
        /// <value>the value without SAP of the component</value>
        public Double PillowRetentWithoutSAP { get; set; }

        /// <summary>
        ///     Gets or sets the value of Pillow Weight without SAP of the component
        /// </summary>
        /// <value>the value without SAP of the component</value>
        public Double PillowWeightWithoutSAP { get; set; }

        /// <summary>
        ///     Gets or sets the value of Cellulose Retention of the component
        /// </summary>
        /// <value>the value Cellulose Retention of the component</value>
        public Double CelluloseRetention { get; set; }

        /// <summary>
        ///     Gets or sets the type of the component
        /// </summary>
        /// <value>the type of the component</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String ComponentType { get; set; }

        /// <summary>
        ///     Gets or sets the number of the component
        /// </summary>
        /// <value>the number of the component</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public String ComponentNr { get; set; }

        /// <summary>
        ///     Gets or sets the production order for the component
        /// </summary>
        /// <value>the production order for the component</value>
        public virtual ProductionOrder ProductionOrder { get; set; }

        #endregion
    }
}