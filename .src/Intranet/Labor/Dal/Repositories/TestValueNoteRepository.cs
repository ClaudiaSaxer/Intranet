#region Usings

using Intranet.Common;
using Intranet.Labor.Model;

#endregion

namespace Intranet.Labor.Dal
{
    /// <summary>
    ///     The Repository for the Errors
    /// </summary>
    public class TestValueNoteRepository : GenericRepository<LaborContext, TestValueNote>
    {
        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public TestValueNoteRepository( IDatabaseFactory<LaborContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( databaseFactory, loggerFactory )
        {
        }

        #endregion
    }
}