using System;
using Extend;
using Intranet.Definition;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class Representing the Service for The CheckDisable
    /// </summary>
    public class CheckDisableService : ICheckDisableService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the check disable.
        /// </summary>
        public ICheckDisableBll CheckDisableBll { get; set; }

        #endregion

        #region Implementation of ICheckDisableService

        /// <summary>
        ///     checks if the modul is visible in the db.
        ///     If the modul doesnt exist, it will return false
        /// </summary>
        /// <param name="modulName">The name of the modul</param>
        /// <returns>The visible status</returns>
        public Boolean IsVisible( String modulName )
        {
            var module = CheckDisableBll.GetModule( modulName );
            if ( module.IsNull() || module.Visible.IsNull() )
                return false;

            return module.Visible ?? false;
        }

        #endregion
    }
}