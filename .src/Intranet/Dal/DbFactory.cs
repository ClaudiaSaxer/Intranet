using System;
using System.Data.Entity;
using Intranet.Common.Disposable;
using Intranet.Definition;

namespace Intranet.Dal
{
    /// <summary>
    ///     Class creating database contexts.
    /// </summary>
    public class DbFactory<TContext> : Disposable, IDatabaseFactory<TContext>
        where TContext : DbContext, new()
    {
        #region Fields

        /// <summary>
        ///     Object used for lock statements.
        /// </summary>
        private readonly Object _syncRoot = new Object();

        /// <summary>
        ///     The database context.
        /// </summary>
        private TContext _dbContext;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="DbFactory{TContext}" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public DbFactory( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(DbFactory<TContext>) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="DbFactory{TContext}" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        public DbFactory( ILogger logger )
            : base( logger )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IDatabaseFactory

        /// <summary>
        ///     Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        public TContext GetDb()
        {
            lock ( _syncRoot )
            {
                if ( _dbContext != null )
                    return _dbContext;

                _dbContext = CreateContext();
                return _dbContext;
            }
        }

        #endregion

        #region Protected Members

        /// <summary>
        ///     Creates a DbContext of type <typeparamref name="TContext" />.
        /// </summary>
        /// <exception cref="InvalidOperationException">Failed to create DbContext.</exception>
        /// <returns>A new DbContext of type <typeparamref name="TContext" />.</returns>
        protected virtual TContext CreateContext() 
            => new TContext();

        #endregion

        #region Implementation of Disposable

        /// <summary>
        ///     Dispose all managed resources.
        /// </summary>
        protected override void Disposed() 
            => _dbContext?.Dispose();

        #endregion
    }
}