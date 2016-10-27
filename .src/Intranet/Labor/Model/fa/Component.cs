using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Labor.Model.fa
{
    /// <summary>
    ///     Class representing a component
    /// </summary>
    public class Component
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the component
        /// </summary>
        /// <value>the id of the component</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 ComponentId { get; set; }

        /// <summary>
        ///     Gets or sets the SAP
        /// </summary>
        /// <value>the SAP value of the component</value>
        public Double SAP { get; set; }

        /// <summary>
        ///     Gets or sets the value without SAP of the component
        /// </summary>
        /// <value>the value without SAP of the component</value>
        public Double WithoutSAP { get; set; }

        /// <summary>
        ///     Gets or sets the type of the component
        /// </summary>
        /// <value>the type of the component</value>
        public String ComponentType { get; set; }

        /// <summary>
        ///     Gets or sets the number of the component
        /// </summary>
        /// <value>the number of the component</value>
        public String ComponentNr { get; set; }

        /// <summary>
        /// Gets or sets the production order for the component
        /// </summary>
        /// <value>the production order for the component</value>
        public ProductionOrder ProductionOrder { get; set; }
        #endregion
    }
}