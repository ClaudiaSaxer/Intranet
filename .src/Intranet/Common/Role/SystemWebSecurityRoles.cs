﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace Intranet.Common
{
    /// <summary>
    ///     Roles for System.Web.Security .
    /// </summary>
    public class SystemWebSecurityRoles : IRoles
    {
        #region Implementation of IRoleFactory

        /// <summary>
        ///     The Roles for the current User
        /// </summary>
        /// <returns>the current user roles</returns>
        public IEnumerable<String> GetRolesForUser() => Roles.GetRolesForUser();

        /// <summary>
        ///     Computes if the user can Edit the Baby Diaper
        /// </summary>
        /// <returns></returns>
        public Boolean CanUserEditLabor()
            => GetRolesForUser()
                .ToList()
                .Any(s => s.Equals("LaborUser") || s.Equals("LaborAdmin"));
        #endregion
    }
}