using Intranet.Common;
using Intranet.Definition;

namespace Intranet.Labor.Dal
{
    /// <summary>
    /// The Repository for the Labor Module
    /// </summary>
    public class LaborRepository : GenericRepository<LaborContext, Model.Labor>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborRepository( IDatabaseFactory<LaborContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
            Logger.Trace( "Enter Ctor - Exit on next line." );
        }

        #endregion
    }
}