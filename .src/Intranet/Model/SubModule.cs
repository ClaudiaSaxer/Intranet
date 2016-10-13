﻿#region Usings

using System.Collections.Generic;
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
        ///     Gets or sets Collection of Submodules to this module
        /// </summary>
        /// <value>The submodules of the module.</value>
        public virtual ICollection<SubModule> SubModules { get; set; }

        /// <summary>
        /// Gets or sets MainModul
        /// </summary>
        /// <value>the mainmodule of the submodule</value>
        public MainModule MainModule { get; set; }

        /// <summary>
        ///     Gets or sets Collection of role types to this module
        /// </summary>
        /// <value>The roletypes of the module.</value>
        public virtual ICollection<Role> Roles { get; set; }

        #endregion
    }
}