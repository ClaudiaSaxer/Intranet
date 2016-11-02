using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Common;
using Intranet.Labor.Model.labor;

namespace Intranet.Labor.Dal.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class TestSheetRepository : GenericRepository<LaborContext, TestSheet>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public TestSheetRepository(IDatabaseFactory<LaborContext> databaseFactory, ILoggerFactory loggerFactory)
            : base( databaseFactory, loggerFactory )
        {
        }

        #endregion
    }
}
