﻿using System;
using System.Collections.Generic;
using System.Web.Security;
using Extend;

namespace Intranet.Common.Bll
{
    /// <summary>
    ///     Class representing the base class for all services
    /// </summary>
    public class ServiceBase: LoggingBase
    {
        #region Properties

        /// <summary>
        ///     The Roles for the current user
        /// </summary>
        public IEnumerable<String> RolesForUser { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        /// <param name="roles">The roles for the current user</param>
        public ServiceBase( ILogger logger, IRoles roles)
            : base( logger )
        {
            RolesForUser = roles.GetRolesForUser();
            logger.ThrowIfNull( nameof( logger ) );
        }

        #endregion
    }
}