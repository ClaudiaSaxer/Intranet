using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Intranet.Definition
{
    /// <summary>
    ///     Generic base class for a repository.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context used by the repository.</typeparam>
    /// <typeparam name="TEntity">The type of the entities in the repository.</typeparam>
    [DebuggerStepThrough]
    public abstract class GenericRepository<TContext, TEntity> : DisposableBase, IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        #region Fields

        /// <summary>
        ///     Gets or sets the internal used database context.
        /// </summary>
        /// <value>The internal used database context.</value>
        private TContext _context;

        /// <summary>
        ///     The DbSet of the repositories table.
        /// </summary>
        private DbSet<TEntity> _dbSet;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        protected virtual TContext Context
        {
            get
            {
                if ( _context == null )
                {
                    Logger.Debug( "Request DbContext form database factory." );
                    _context = DatabaseFactory.GetDb();
                }
                return _context;
            }
        }

        /// <summary>
        ///     Gets the database factory used by the repository.
        /// </summary>
        /// <value>The database factory used by the repository.</value>
        protected virtual IDatabaseFactory<TContext> DatabaseFactory { get; }

        /// <summary>
        ///     Gets or set a <see cref="IDbCommit{TContext}" />.
        /// </summary>
        /// <value>A <see cref="IDbCommit{TContext}" />.</value>
        // ReSharper disable once MemberCanBePrivate.Global
        public IDbCommit<TContext> DbCommit { get; set; }

        /// <summary>
        ///     Gets the DbSet of the repositories table.
        /// </summary>
        /// <value>The DbSet of the repositories table.</value>
        private DbSet<TEntity> DbSet
        {
            get { return _dbSet ?? ( _dbSet = Context.Set<TEntity>() ); }
        }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="GenericRepository{TContext, TEntity}" /> class.
        /// </summary>
        /// <param name="databaseFactory">A <see cref="IDatabaseFactory{TContext}" />.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        protected GenericRepository( IDatabaseFactory<TContext> databaseFactory, ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(GenericRepository<TContext, TEntity>) ) )
        {
            Logger.Trace( "Enter Ctor - Exit on next line." );
            DatabaseFactory = databaseFactory;
        }

        #endregion

        #region Implementation of Disposable

        /// <summary>
        ///     Dispose all managed resources.
        /// </summary>
        protected override void Disposed()
            => _context?.Dispose();

        #endregion

        #region Implementation of IGenericRepository{T}

        /// <summary>
        ///     Gets all entities.
        /// </summary>
        /// <returns>All entities</returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet;
        }

        /// <summary>
        ///     Gets the entities matching the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The entities matching the given predicate.</returns>
        public virtual IQueryable<TEntity> Where( Expression<Func<TEntity, Boolean>> predicate )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return GetAll()
                .Where( predicate );
        }

        /// <summary>
        ///     Adds the given entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>Returns the added entity.</returns>
        public virtual TEntity Add( TEntity entity )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.Add( entity );
        }

        /// <summary>
        ///     Adds the given collection of entities into context underlying the set with
        ///     each entity being put into the Added state such that it will be inserted
        ///     into the database when SaveChangesAsync is called.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        /// <returns>The collection of entities.</returns>
        public virtual IEnumerable<TEntity> AddRange( IEnumerable<TEntity> entities )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.AddRange( entities );
        }

        /// <summary>
        ///     Removes the given entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>Returns the removed entity.</returns>
        public virtual TEntity Remove( TEntity entity )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.Remove( entity );
        }

        /// <summary>
        ///     Sets the state of the given entity to modified.
        /// </summary>
        /// <param name="entity">The entity to mark as modified.</param>
        public virtual void SetModified( TEntity entity )
        {
            Logger.Trace( "Enter - Exit after next line." );

            Entry( entity )
                .State = EntityState.Modified;
        }

        /// <summary>
        ///     Attaches the given entity to the current database context.
        /// </summary>
        /// <param name="entity">The entity to attach.</param>
        /// <returns>Returns the attached entity.</returns>
        public virtual TEntity Attach( TEntity entity )
        {
            Logger.Trace( "Enter - Exit after next line." );

            return DbSet.Attach( entity );
        }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of Objects written to the underlying database.</returns>
        public virtual Int32 SaveChanges()
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbCommit.Commit();
        }

        /// <summary>
        ///     Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
        /// <returns>
        ///     A task that represents the asynchronous save operation.
        ///     The task result contains the number of Objects written to the underlying database.
        /// </returns>
        public virtual Task<Int32> SaveChangesAsync( CancellationToken? cancellationToken = null )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return cancellationToken == null
                ? DbCommit.CommitAsync()
                : DbCommit.CommitAsync( cancellationToken.Value );
        }

        /// <summary>
        ///     Gets a <see cref="System.Data.Entity.Infrastructure.DbEntityEntry{TEntity}" /> Object for
        ///     the given entity providing access to information about the entity and the
        ///     ability to perform actions on the entity.
        /// </summary>
        /// <typeparam name="TAnyEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns an entry for the entity.</returns>
        public virtual DbEntityEntry<TAnyEntity> Entry<TAnyEntity>( TAnyEntity entity ) where TAnyEntity : class
        {
            Logger.Trace( "Enter - Exit on next line." );

            return Context.Entry( entity );
        }

        /// <summary>
        ///     Gets the number of records of the repositories table.
        /// </summary>
        /// <returns>Returns the number records of the repositories table.</returns>
        public virtual Int32 CountRecords()
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.Count();
        }

        /// <summary>
        ///     Gets the number of records of the repositories table asynchronously.
        /// </summary>
        /// <returns>Returns the number records of the repositories table.</returns>
        public virtual Task<Int32> CountRecordsAsync()
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.CountAsync();
        }

        /// <summary>
        ///     Asynchronously finds an entity with the given primary key values.  If an
        ///     entity with the given primary key values exists in the context, then it is
        ///     returned immediately without making a request to the store. Otherwise, a
        ///     request is made to the store for an entity with the given primary key values
        ///     and this entity, if found, is attached to the context and returned. If no
        ///     entity is found in the context or the store, then null is returned.
        /// </summary>
        /// <remarks>
        ///     The ordering of composite key values is as defined in the EDM, which is in
        ///     turn as defined in the designer, by the Code First fluent API, or by the
        ///     DataMember attribute.  Multiple active operations on the same context instance
        ///     are not supported. Use 'await' to ensure that any asynchronous operations
        ///     have completed before calling another method on this context.
        /// </remarks>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>A task that represents the asynchronous find operation. The task result contains the entity found, or null.</returns>
        public virtual Task<TEntity> FindAsync( params Object[] keyValues )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.FindAsync( keyValues );
        }

        /// <summary>
        ///     Creates a raw SQL query that will return entities in this set. By default,
        ///     the entities returned are tracked by the context; this can be changed by
        ///     calling AsNoTracking on the <see cref="System.Data.Entity.Infrastructure.DbSqlQuery{TEntity}" />
        ///     returned.  Note that the entities returned are always of the type for this
        ///     set and never of a derived type. If the table or tables queried may contain
        ///     data for other entity types, then the SQL query must be written appropriately
        ///     to ensure that only entities of the correct type are returned.  As with any
        ///     API that accepts SQL it is important to parameterize any user input to protect
        ///     against a SQL injection attack. You can include parameter place holders in
        ///     the SQL query String and then supply parameter values as additional arguments.
        ///     Any parameter values you supply will automatically be converted to a DbParameter.
        ///     context.Blogs.SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor);
        ///     Alternatively, you can also construct a DbParameter and supply it to SqlQuery.
        ///     This allows you to use named parameters in the SQL query string .
        ///     context.Blogs.SqlQuery("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author",
        ///     userSuppliedAuthor));
        /// </summary>
        /// <param name="sql">The SQL query string .</param>
        /// <param name="parameters">
        ///     The parameters to apply to the SQL query string . If output parameters are
        ///     used, their values will not be available until the results have been read
        ///     completely. This is due to the underlying behavior of DbDataReader, see
        ///     http://go.microsoft.com/fwlink/?LinkID=398589
        ///     for more details.
        /// </param>
        /// <returns>
        ///     A <see cref="System.Data.Entity.Infrastructure.DbSqlQuery{TEntity}" /> Object that will execute the query when
        ///     it is enumerated.
        /// </returns>
        public virtual DbSqlQuery<TEntity> SqlQuery( String sql, params Object[] parameters )
        {
            Logger.Trace( "Enter - Exit on next line." );

            return DbSet.SqlQuery( sql, parameters );
        }

        #endregion Implementation of IGenericRepository{T}
    }
}