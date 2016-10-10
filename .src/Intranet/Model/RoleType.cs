#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Class Representing a RoleType
    /// </summary>
    public class RoleType
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the id of a role type
        /// </summary>
        /// <value>The role id of the roletype.</value>
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public Int32 RoleTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the boolean isreadonly.
        ///     If true, Role only has read access.
        ///     If false, Role has read and edit access.
        /// </summary>
        /// <value>The read only boolean of the roletype.</value>
        public Boolean IsReadOnly { get; set; }

        /// <summary>
        ///     Gets or sets the role
        /// </summary>
        /// <value>The role of the roletype.</value>
        [ForeignKey( "RoleId" )]
        public virtual Role Role { get; set; }

        /// <summary>
        ///     Gets or sets the role id of the roletype.
        /// </summary>
        /// <value>The role id of the roletype.</value>
        [Index( "IX_RoleType_RoleId", IsUnique = false )]
        public Int32 RoleId { get; set; }

        /// <summary>
        ///     Gets or sets the module
        /// </summary>
        /// <value>The module of the roletype.</value>
        [ForeignKey( "ModuleId" )]
        public virtual Module Module { get; set; }

        /// <summary>
        ///     Gets or sets the module id of the roletype.
        /// </summary>
        /// <value>The module id of the roletype.</value>
        [Index( "IX_RoleType_ModuleId", IsUnique = false )]
        public Int32 ModuleId { get; set; }

        #endregion
    }
}