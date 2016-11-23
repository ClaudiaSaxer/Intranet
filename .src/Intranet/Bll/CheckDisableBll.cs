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
    ///     Class representing the bll of the checkdisable.
    /// </summary>
    public class CheckDisableBll : ICheckDisableBll
    {
        /// <summary>
        /// Repository for Modules
        /// </summary>
        public IGenericRepository<Module> ModuleRepository { get; set; }

        #region Implementation of ICheckDisableBll

        /// <summary>
        ///     Returns the Module with the name if it exist.
        ///     If it doesnt exist, it will be null
        /// </summary>
        /// <param name="name">The name of the Modul</param>
        /// <returns>The Module if it exist (or null)</returns>
        public Module GetModule( String name ) => ModuleRepository.GetAll(  )
                                                                .FirstOrDefault(m => m.Name.Equals( name ));

        #endregion
    }
}
