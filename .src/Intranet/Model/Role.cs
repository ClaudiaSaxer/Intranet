#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Class representing a Role
    /// </summary>
    public class Role
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of the role
        /// </summary>
        /// <value>The id of the Role.</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 RoleId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the role
        /// </summary>
        /// <value>The name of the Role.</value>
        public String Name { get; set; }

        /// <summary>
        ///     Gets or sets a collection of roletypes for the role
        /// </summary>
        /// <value>The roletypes of the Role.</value>
        public virtual ICollection<RoleType> RoleTypes { get; set; }

        /// <summary>
        ///     Gets or sets the roletype id of the role.
        /// </summary>
        /// <value>The roletype id of the Role.</value>
        [Index( "IX_Role_RoleTypesId", IsUnique = false )]
        public Int32 RoleTypeId { get; set; }

        #endregion
    }
}