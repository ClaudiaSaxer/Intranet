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
        [UniqueKey]
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
        public String ActionName { get; set; }

        /// <summary>
        ///     Gets or sets the path to the module start page
        /// </summary>
        /// <value>The path of the module.</value>
        public String ControllerName { get; set; }

        /// <summary>
        ///     Gets or sets the are to the module
        /// </summary>
        /// <value>The are of the module.</value>
        public String AreaName { get; set; }

        /// <summary>
        ///     If Main Module is Visible in Shell or not
        /// </summary>
        /// <value>The visability of the module. Null if type is not main</value>
        public Boolean? Visible { get; set; }

        /// <summary>
        ///     Gets or sets the are to the module
        /// </summary>
        /// <value>The are of the module.</value>
        public ModuleType Type { get; set; }

        /// <summary>
        ///     Gets or sets Collection of Submodules to this module
        /// </summary>
        /// <value>The submodules of the module.</value>
        public virtual ICollection<Module> Submodules { get; set; }

        /// <summary>
        ///     Gets or sets Collection of role types to this module
        /// </summary>
        /// <value>The roletypes of the module.</value>
        public virtual ICollection<Role> Roles { get; set; }

        #endregion
    }
}