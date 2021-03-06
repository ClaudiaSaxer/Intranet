﻿#region Usings

using System;
using System.Data.Entity;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Interface representing a class creating database contexts.
    /// </summary>
    public interface IDatabaseFactory<out TContext> : IDisposable
        where TContext : DbContext, new()
    {
        /// <summary>
        ///     Gets a database context.
        /// </summary>
        /// <returns>Returns a database context.</returns>
        TContext GetDb();
    }
}