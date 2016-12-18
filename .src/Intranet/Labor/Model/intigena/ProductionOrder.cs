#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Labor.Model
{
    /// <summary>
    ///     The Class representing the production order
    /// </summary>
    public class ProductionOrder
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the production order
        /// </summary>
        /// <value>the id of the production order</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 FaId { get; set; }

        /// <summary>
        ///     Gets or sets the number of the production order
        /// </summary>
        /// <value>the number of the production order</value>
        [Column( TypeName = "varchar" )]
        [StringLength( 255 )]
        public String FaNr { get; set; }

        /// <summary>
        ///     Gets or sets the start Date and Time of the production order
        /// </summary>
        /// <value>The start date and time of the production order</value>
        [Column( TypeName = "datetime2" )]
        public DateTime StartDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the end Data and Time of the production order
        /// </summary>
        /// <value>The end date and time of the production order</value>
        [Column( TypeName = "datetime2" )]
        public DateTime EndDateTime { get; set; }

        /// <summary>
        ///     Gets or sets the article for this production order <see cref="Article" />
        /// </summary>
        /// <value>the article for the production order</value>
        public virtual Article Article { get; set; }

        /// <summary>
        ///     Gets or sets the article ref id
        /// </summary>
        /// <value>the ref id of the article</value>
        [ForeignKey( "Article" )]
        [Index( "IX_ProductionOrder_ArticleId" )]
        public Int32 ArticleId { get; set; }

        /// <summary>
        ///     Gets or sets the machine for the production order
        /// </summary>
        /// <value>the machine for the production order</value>
        [ForeignKey( "MachineRefId" )]
        public virtual Machine Machine { get; set; }

        /// <summary>
        ///     Gets or sets the machine ref id
        /// </summary>
        /// <value>the ref id of the machine</value>
        [ForeignKey( "Machine" )]
        [Index( "IX_ProductionOrder_MachineId" )]
        public Int32 MachineId { get; set; }

        /// <summary>
        ///     Gets or sets the component for the production order
        /// </summary>
        /// <value>the component for the production order</value>
        public virtual ProductionOrderComponent Component { get; set; }

        #endregion
    }
}