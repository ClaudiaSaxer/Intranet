#region Usings

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing a class able to commit changes to the database.
    /// </summary>
    public interface IDbCommit<TContext> : IDisposable
    {
        /// <summary>
        ///     Commits all pending changes to the database.
        /// </summary>
        /// <returns>Returns the number of changed records.</returns>
        Int32 Commit();

        /// <summary>
        ///     Commits all pending changes to the database asynchronously.
        /// </summary>
        /// <returns>Returns the number of changed records asynchronously.</returns>
        Task<Int32> CommitAsync( CancellationToken? cancellationToken = null );
    }
}