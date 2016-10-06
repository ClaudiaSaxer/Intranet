using System;

namespace Intranet.Bll
{
    /// <summary>
    /// Local Optimistic Concurrency Exception
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LocalOptimisticConcurrencyException<T> : Exception
    {
        #region Properties

        /// <summary>
        /// Gets and Sets the Merged Entity
        /// </summary>
        public T MergedEntity { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Constructs the LocalOptimisticConcurrencyException
        /// </summary>
        /// <param name="msg">The message to be set</param>
        public LocalOptimisticConcurrencyException( String msg )
            : base( msg )
        {
        }

        /// <summary>
        /// Constructs the LocalOptimisticConcurrencyException
        /// </summary>
        /// <param name="msg">The message to be set</param>
        /// <param name="mergedEntity">the merged Entity</param>
        public LocalOptimisticConcurrencyException( String msg, T mergedEntity )
            : base( msg )
        {
            MergedEntity = mergedEntity;
        }

        #endregion
    }
}