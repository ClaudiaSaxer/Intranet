#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Class representing a Module
    /// </summary>
    [Table( "MainModule" )]
    public class MainModule : Module
    {
        #region Properties

        /// <summary>
        ///     If Main Module is Visible in Shell or not
        /// </summary>
        public Boolean Visible { get; set; }

        /// <summary>
        ///     Gets or sets Collection of Submodules to this module
        /// </summary>
        /// <value>The submodules of the module.</value>
        public virtual ICollection<SubModule> SubModules { get; set; }

        /// <summary>
        ///     Gets or sets Collection of role types to this module
        /// </summary>
        /// <value>The roletypes of the module.</value>
        public virtual ICollection<Role> Roles { get; set; }

        #endregion
    }
}