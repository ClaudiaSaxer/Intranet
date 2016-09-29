using Intranet.Definition.Dal;
using Intranet.Definition.Logger;
using Intranet.Model;

namespace Intranet.Dal.Repositories
{
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