#region Usings

using Intranet.Common;
using Intranet.Labor.Model.labor;

#endregion

namespace Intranet.Labor.Dal.Repositories
{
    /// <summary>
    ///     The Repository for the IncontinencePadTestValueRepository
    /// </summary>
    public class IncontinencePadTestValueRepository : GenericRepository<LaborContext, IncontinencePadTestValue>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public IncontinencePadTestValueRepository( IDatabaseFactory<LaborContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }

        #endregion
    }
}