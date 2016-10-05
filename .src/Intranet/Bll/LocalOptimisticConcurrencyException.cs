using System;

namespace Intranet.Bll
{
    public class LocalOptimisticConcurrencyException<T> : Exception
    {
        #region Properties

        public T MergedEntity { get; set; }

        #endregion

        #region Ctor

        public LocalOptimisticConcurrencyException( String msg )
            : base( msg )
        {
        }

        public LocalOptimisticConcurrencyException( String msg, T mergedEntity )
            : base( msg )
        {
            MergedEntity = mergedEntity;
        }

        #endregion
    }
}