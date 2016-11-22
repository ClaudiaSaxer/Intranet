using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Definition;
using Intranet.Model;

namespace Intranet.Bll
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckDisableBll : ICheckDisableBll
    {
        /// <summary>
        /// Repository for Modules
        /// </summary>
        public IGenericRepository<Module> RoleRepository { get; set; }

        #region Implementation of ICheckDisableBll

        /// <summary>
        ///     Returns the Module with the name if it exist.
        ///     If it doesnt exist, it will be null
        /// </summary>
        /// <param name="name">The name of the Modul</param>
        /// <returns>The Module if it exist (or null)</returns>
        public Module GetModule( String name ) => RoleRepository.Where( m => m.Name.Equals( name ) )
                                                                .FirstOrDefault();

        #endregion
    }
}
