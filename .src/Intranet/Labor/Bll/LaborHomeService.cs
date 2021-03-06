﻿#region Usings

using Intranet.Common;
using Intranet.Labor.Definition;
using Intranet.Labor.ViewModel;

#endregion

namespace Intranet.Labor.Bll
{
    /// <summary>
    ///     Class representing the labor home service
    /// </summary>
    public class LaborHomeService : ServiceBase, ILaborHomeService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the labor home.
        /// </summary>
        public ILaborHome LaborHome { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="ServiceBase" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        public LaborHomeService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(LaborHomeService) ) )
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of ILaborHomeService

        /// <summary>
        ///     All labor submodules that the current User is allowed to see.
        /// </summary>
        /// <returns>The ViewModel for the home</returns>
        public LaborHomeViewModel GetLaborHomeViewModel()
        {
            var roleNames = Roles.GetRolesForUser();

            var vm = new LaborHomeViewModel
            {
                Modules = LaborHome.AllLaborModulesForRoles( roleNames )
            };
            return vm;
        }

        #endregion
    }
}