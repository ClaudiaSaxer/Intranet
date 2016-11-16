using System;
using System.Linq;
using Intranet.Common;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class representing the service for the navigation.
    /// </summary>
    public class NavigationService : ServiceBase, INavigationService

    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the navigation.
        /// </summary>
        public INavigationBll NavigationBll { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="LoggingBase" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">loggerFactory can not be null</exception>
        /// <param name="loggerFactory"><see cref="ILoggerFactory" />.</param>
        public NavigationService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(NavigationService) ) )
        {
        }

        #endregion

        #region Implementation of INavigationService

        /// <summary>
        ///     All main models that the current User is allowed to see.
        /// </summary>
        /// <returns>The ViewModel for the navigation</returns>
        public NavigationViewModel GetNavigationViewModel()
        {
            var roleNames = Roles.GetRolesForUser()
                                 .ToArray();

            var vm = new NavigationViewModel
            {
                MainModules = NavigationBll.AllVisibleMainModulesForRoles( roleNames ),
                SettingModules = NavigationBll.AllSettingsForRoles( roleNames )
            };
            return vm;
        }

        #endregion

        #region Implementation of INavigationService

        #endregion
    }
}