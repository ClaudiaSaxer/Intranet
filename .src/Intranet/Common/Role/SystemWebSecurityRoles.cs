using System;
using System.Collections.Generic;
using System.Web.Security;

namespace Intranet.Common
{
    /// <summary>
    ///     A SystemWebSecurityRoles Role factory.
    /// </summary>
    public class SystemWebSecurityRoles : IRoles
    {
        #region Implementation of IRoleFactory

        /// <summary>
        ///     The Roles for the current User
        /// </summary>
        /// <returns>the current user roles</returns>
        public IEnumerable<String> GetRolesForUser() => Roles.GetRolesForUser();

        #endregion
    }
}