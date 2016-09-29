using System;
using Extend;
using Intranet.Definition.Logger;

namespace Intranet.Common.Disposable
{
    /// <summary>
    ///     Base class for disposable Objects.
    /// </summary>
    public abstract class Disposable : LoggingBase, IDisposable
    {
        #region Fields

        /// <summary>
        ///     Stores whether the instance is disposed or not.
        /// </summary>
        private Boolean _disposed;

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="Disposable" /> class.
        /// </summary>
        /// <param name="logger">A <see cref="ILogger" />.</param>
        protected Disposable( ILogger logger )
            : base( logger )
        {
            logger.ThrowIfNull( () => logger );

            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Destructor

        /// <summary>
        ///     Destructs the instance.
        /// </summary>
        ~Disposable()
        {
            Logger?.Debug( "Destruct instance." );

            Dispose( false );
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        ///     Dispose the current instance.
        /// </summary>
        public void Dispose()
        {
            Logger.Trace( "Dispose instance." );

            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        ///     Releases all managed resources hold by this instance.
        /// </summary>
        /// <param name="disposing">A value indicating whether the dispose method or the destructor is calling.</param>
        private void Dispose( Boolean disposing )
        {
            if ( disposing && !_disposed )
                Disposed();

            _disposed = true;
        }

        /// <summary>
        ///     Method invoked when the instance gets disposed.
        /// </summary>
        protected abstract void Disposed();

        #endregion
    }
}