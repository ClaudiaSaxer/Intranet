using System;

namespace Intranet.Definition
{
    /// <summary>
    ///     Interface representing the service for the checkdisable
    /// </summary>
    public interface ICheckDisableService
    {
        /// <summary>
        ///     checks if the modul is visible in the db.
        ///     If the modul doesnt exist, it will return false
        /// </summary>
        /// <param name="modulName">The name of the modul</param>
        /// <returns>The visible status</returns>
        Boolean IsVisible( String modulName );
    }
}