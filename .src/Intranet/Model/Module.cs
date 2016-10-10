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
    public class Module
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the Id of the module
        /// </summary>
        /// <value>The module id of the module.</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 ModuleId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the module
        /// </summary>
        /// <example>
        ///     name example: Labor
        /// </example>
        /// <value>The name of the module.</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets a description of the module
        /// </summary>
        /// <value>The description of the module.</value>
        public String Description { get; set; }

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String Path { get; set; }

        /// <summary>
        ///     Gets or sets Collection of role types to this module
        /// </summary>
        /// <value>The roletypes of the module.</value>
        public virtual ICollection<RoleType> RoleTypes { get; set; }

        /// <summary>
        ///     Gets or sets the roletype id of the module.
        /// </summary>
        /// <value>The roletype id of the module.</value>
        [Index( "IX_Module_RoleTypeId", IsUnique = false )]
        public Int32 RoleTypeId { get; set; }

        /// <summary>
        ///     Gets or sets Collection of Submodules to this module
        /// </summary>
        /// <value>The submodules of the module.</value>
        public virtual ICollection<Module> Submodules { get; set; }

        #endregion
    }
}