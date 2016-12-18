#region Usings

using System;

#endregion

namespace Intranet.Common
{
    /// <summary>
    ///     Class Representing Settings for the Roles
    /// </summary>
    public static class RoleSettings
    {
        #region Constants

        /// <summary>
        ///     The Admin of the Labor, can do everything in the Labor. Inclussive Settings.
        /// </summary>
        public const String LaborAdmin = "Everyone";

        /// <summary>
        ///     The User of the Labor, only has read and write access.
        /// </summary>
        public const String LaborUser = "LaborUser";

        /// <summary>
        ///     The Viewer of the Labor, only has read access.
        /// </summary>
        public const String LaborViewer = "LaborViewer";

        #endregion
    }
}