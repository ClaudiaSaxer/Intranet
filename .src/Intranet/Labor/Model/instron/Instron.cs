﻿#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Class representing Instron
    /// </summary>
    public class Instron
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Id of Instron
        /// </summary>
        /// <value>The Id for Instron</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 IntstronId { get; set; }

        /// <summary>
        ///     Gets or sets the ProductionOrder Number
        /// </summary>
        /// <value>The ProductionOrder Number of Instron.</value>
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        [Index("IX_Instron_FaNr", IsUnique = true)]
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the DateTime from the Instron Test
        /// </summary>
        /// <value>The Date and Time from the Test</value>
        [Column(TypeName = "datetime2")]
        public DateTime TestDateTime { get; set; }

        /// <summary>
        ///     Gets or sets a Collection from Instron Values <see cref="InstronValues" />
        /// </summary>
        /// <value>The Values for the Instron</value>
        public virtual ICollection<InstronValue> InstronValues { get; set; }

        #endregion
    }
}