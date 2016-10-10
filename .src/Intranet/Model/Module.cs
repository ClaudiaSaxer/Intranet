#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Intranet.Model
{
    /// <summary>
    ///     Module that will be in the IntranetShell
    /// </summary>
    public class Module
    {
        #region Properties

        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        [Column( "Id" )]
        public Guid ModuleId { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }
        public String Path { get; set; }


        /// <summary>
        /// Collection of Submodules to this Module
        /// </summary>
        public virtual ICollection<Submodule> Submodules { get; set; }

        #endregion
    }
}