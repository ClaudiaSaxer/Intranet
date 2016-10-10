using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Definition;
using Intranet.Model;

namespace Intranet.Dal.Repositories
{
    /// <summary>
    /// Repository for the Role Types
    /// </summary>
    public class RoleTypeRepository: GenericRepository<IntranetContext, RoleType>
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public RoleTypeRepository( IDatabaseFactory<IntranetContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }
    }
}
