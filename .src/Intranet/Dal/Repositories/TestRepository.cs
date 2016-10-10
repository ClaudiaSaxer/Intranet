﻿using Intranet.Common;
using Intranet.Definition;
using Intranet.Model;

namespace Intranet.Dal.Repositories
{
    /// <summary>
    ///     A Test Repository
    ///     to be removed
    /// </summary>
    public class TestRepository : GenericRepository<IntranetContext, Test>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public TestRepository( IDatabaseFactory<IntranetContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
            Logger.Trace( "Enter Ctor - Exit on next line." );
        }

        #endregion
    }
}