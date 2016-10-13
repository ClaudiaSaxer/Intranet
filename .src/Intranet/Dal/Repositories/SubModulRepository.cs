
#region Usings

using Intranet.Definition;
using Intranet.Model;

#endregion

namespace Intranet.Dal.Repositories
{
    /// <summary>
    /// The Repository for the SubModule
    /// </summary>
    public class SubModuleRepository : GenericRepository<IntranetContext, SubModule>
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public SubModuleRepository( IDatabaseFactory<IntranetContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }
    }
}