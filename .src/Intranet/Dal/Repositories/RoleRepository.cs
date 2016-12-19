#region Usings

using Intranet.Common;
using Intranet.Model;

#endregion

namespace Intranet.Dal
{
    /// <summary>
    ///     Repository for the Roles
    /// </summary>
    public class RoleRepository : GenericRepository<IntranetContext, Role>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public RoleRepository( IDatabaseFactory<IntranetContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }

        #endregion
    }
}