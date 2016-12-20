#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing the machine
    /// </summary>
    public class Machine
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the machine
        /// </summary>
        /// <value>the id of the machine</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 MachineId { get; set; }

        /// <summary>
        ///     Gets or sets the number of the machine
        /// </summary>
        /// <value>the number of the machine</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        [Index("IX_Machine_MachineNr", IsUnique = true)]
        public String MachineNr { get; set; }

        /// <summary>
        ///     Gets or sets the production orders associated with this machine <see cref="ProductionOrders" />
        /// </summary>
        /// <value>the production orders associated with this machine</value>
        public ICollection<ProductionOrder> ProductionOrders { get; set; }

        #endregion
    }
}