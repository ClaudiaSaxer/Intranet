#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Class representing a Module
    /// </summary>
    [Table( "SubModule" )]
    public class SubModule : Module
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Id of the module
        /// </summary>
        /// <value>The module id of the module.</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 SubModuleId { get; set; }

        /// <summary>
        ///     Gets or sets Collection of Submodules to this module
        /// </summary>
        /// <value>The submodules of the module.</value>
        public virtual ICollection<SubModule> SubModules { get; set; }

        /// <summary>
        ///     Gets or sets MainModul
        /// </summary>
        /// <value>The mainmodule of the submodule</value>
        public MainModule MainModule { get; set; }

        /// <summary>
        ///     Gets or sets Collection of role types to this module
        /// </summary>
        /// <value>The roletypes of the module.</value>
        public virtual ICollection<Role> Roles { get; set; }

        #endregion
    }
}