#region Usings

using System;
using Intranet.Model;

#endregion

namespace Intranet.Definition
{
    /// <summary>
    ///     Interface representing the bll for the checkdisable
    /// </summary>
    public interface ICheckDisableBll
    {
        /// <summary>
        ///     Returns the Module with the name if it exist.
        ///     If it doesnt exist, it will be null
        /// </summary>
        /// <param name="name">The name of the Modul</param>
        /// <returns>The Module if it exist (or null)</returns>
        Module GetModule( String name );
    }
}