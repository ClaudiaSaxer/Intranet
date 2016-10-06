using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Intranet.Common.Disposable;
using Intranet.Definition;

namespace Intranet.Common
{
    /// <summary>
    ///     Class responsible for committing pending changes to the database.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class DbCommit<TContext> : Disposable.Disposable, IDbCommit<TContext>
        where TContext : DbContext, new()
    {
        #region Fields

        /// <summary>
        ///     A database factory.
        /// </summary>
        private readonly IDatabaseFactory<TContext> _databaseFactory;

        /// <summary>
        ///     The current database context.
        /// </summary>
        private TContext _context;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a database context.
        /// </summary>
        /// <value>A database context.</value>
        private TContext DbContext
        {
            get
            {
                if ( _context == null )
                {
                    Logger.Debug( "Request DbContext from database factory." );
                    _context = _databaseFactory.GetDb();
                }
                return _context;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="DbCommit{TContext}" /> class.
        /// </summary>
        /// <param name="databaseFactory"></param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public DbCommit( IDatabaseFactory<TContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(DbCommit<TContext>) ) )
        {
            Logger.Trace( "Enter Ctor - Exit after next line." );

            _databaseFactory = databaseFactory;
        }

        #endregion

        #region Implementation of Disposable

        /// <summary>
        ///     Dispose all managed resources.
        /// </summary>
        protected override void Disposed() => _context?.Dispose();

        #endregion

        #region Implementation of IDbCommit

        /// <summary>
        ///     Commits all pending changes to the database.
        /// </summary>
        /// <returns>Returns the number of changed records.</returns>
        public Int32 Commit()
        {
            Logger.Trace( "Enter - Exit on next line." );
            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Commits all pending changes to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Returns the number of changed records asynchronously.</returns>
        public Task<Int32> CommitAsync( CancellationToken? cancellationToken = null )
        {
            Logger.Trace( "Enter - Exit on next line." );
            return cancellationToken == null
                ? DbContext.SaveChangesAsync()
                : DbContext.SaveChangesAsync( cancellationToken.Value );
        }

        #endregion
    }
}