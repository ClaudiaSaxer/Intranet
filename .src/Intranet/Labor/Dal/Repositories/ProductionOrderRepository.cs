#region Usings

using Intranet.Common;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Dal.Repositories
{
    /// <summary>
    ///     The Repository for the ProductionOrders
    /// </summary>
    public class ProductionOrderRepository : GenericRepository<LaborContext, ProductionOrder>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public ProductionOrderRepository( IDatabaseFactory<LaborContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }

        #endregion
    }
}