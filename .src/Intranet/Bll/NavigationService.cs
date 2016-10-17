using System;
using System.Linq;
using System.Web.Security;
using Intranet.Definition;
using Intranet.Definition.Bll;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    /// <summary>
    ///    Class representing the service for the navigation.
    /// </summary>
    public class NavigationService : LoggingBase, INavigationService

    {
        /// <summary>
        /// Gets or sets the view model for the navigation.
        /// </summary>
        /// <value>the viewmodel for the navigation</value>
        public NavigationViewModel NavigationViewModel { get; set; }
        #region Ctor

        /// <summary>
        /// Gets or sets the bll for the navigation.
        /// </summary>
        public INavigationBll NavigationBll { get; set; }

        /// <summary>
        ///     Initialize a new instance of the <see cref="LoggingBase" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">loggerFactory can not be null</exception>
        /// <param name="loggerFactory"><see cref="ILoggerFactory" />.</param>
        public NavigationService(ILoggerFactory loggerFactory)
            : base(loggerFactory.CreateLogger(typeof(NavigationService)))
        {
        }

        #endregion

        #region Implementation of INavigationService


        #endregion

        #region Implementation of INavigationService

        /// <summary>
        /// All main models that the current User is allowed to see. 
        /// </summary>
        /// <returns>The ViewModel for the navigation</returns>
        public NavigationViewModel GetNavigationViewModel()
        {
            var roleNames = Roles.GetRolesForUser();

            var vm = new NavigationViewModel
            {
                Modules = NavigationBll.AllVisibleMainModulesForRoles( roleNames )
            };
            // NavigationViewModel.Modules = NavigationBll.AllVisibleMainModulesForRoles( roleNames ).ToList();
            return vm;

        }

        #endregion
    }
} 