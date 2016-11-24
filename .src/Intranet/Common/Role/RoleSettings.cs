using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Common.Role
{
    /// <summary>
    /// Class Representing Settings for the Roles 
    /// </summary>
    public static class RoleSettings
    {
        /// <summary>
        /// The Admin of the Labor, can do everything in the Labor. Inclussive Settings.
        /// </summary>
        public static String LaborAdmin => "Everyone";
        /// <summary>
        /// The User of the Labor, only has read and write access.
        /// </summary>
        public static String LaborUser => "LaborUser";
        /// <summary>
        /// The Viewer of the Labor, only has read access.
        /// </summary>
        public static String LaborViewer => "LaborViewer";
    }
}
