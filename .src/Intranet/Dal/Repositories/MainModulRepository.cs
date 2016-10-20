﻿#region Usings

using Intranet.Common;
using Intranet.Model;

#endregion

namespace Intranet.Dal.Repositories
{
    /// <summary>
    ///     The Repository for the Module
    /// </summary>
    public class MainModuleRepository : GenericRepository<IntranetContext, MainModule>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public MainModuleRepository( IDatabaseFactory<IntranetContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }

        #endregion
    }
}