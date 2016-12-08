using System;
using System.Collections.Generic;

namespace Intranet.Common
{
    /// <summary>
    ///     Factory for Roles
    /// </summary>
    public interface IRoles
    {
        /// <summary>
        ///     Computes if the user can Edit Labor
        /// </summary>
        /// <returns>true: if the current user is allowed to edit the labor</returns>
        Boolean CanUserEditLabor();

        /// <summary>
        ///     The Roles for the current User
        /// </summary>
        /// <returns>the current user roles</returns>
        IEnumerable<String> GetRolesForUser();
    }
}