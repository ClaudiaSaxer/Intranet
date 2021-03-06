﻿namespace Intranet.Labor.Model
{
    /// <summary>
    ///     Enum representing the RW Type
    /// </summary>
    public enum RwType
    {
        /// <summary>
        ///     the value is in the range and ok
        /// </summary>
        Ok,

        /// <summary>
        ///     the value is better than expected
        /// </summary>
        Better,

        /// <summary>
        ///     the value is worse than expected
        /// </summary>
        Worse,

        /// <summary>
        ///     some value correlationg with the endvalue is worse
        /// </summary>
        SomethingWorse
    }
}