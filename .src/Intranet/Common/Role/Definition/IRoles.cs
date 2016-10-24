using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Intranet.Common
{

    /// <summary>
    ///     Factory for Roles
    /// </summary>
    public interface IRoles
    {
        /// <summary>
        /// The Roles for the current User
        /// </summary>
        /// <param name="isAny"></param>
        /// <returns>the current user roles</returns>
        IEnumerable<String> GetRolesForUser();

    }
}
