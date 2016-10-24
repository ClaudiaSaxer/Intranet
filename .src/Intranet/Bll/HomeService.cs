using System.Web.Security;
using Intranet.Common;
using Intranet.Common.Bll;
using Intranet.Definition;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    /// <summary>
    ///     Class Representing the Service for Home
    /// </summary>
    public class HomeService : ServiceBase, IHomeService
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the bll for the home.
        /// </summary>
        public IHomeBll HomeBll { get; set; }


        #endregion

        #region Ctor

        /// <summary>
        ///     Initialize a new instance of the <see cref="HomeService" /> class.
        /// </summary>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory" />.</param>
        /// <param name="roles">Roles for the current user</param>
        public HomeService( ILoggerFactory loggerFactory, IRoles roles)
            : base( loggerFactory.CreateLogger( typeof(HomeService)),roles)
        {
            Logger.Trace( "Enter Ctor - Exit." );
        }

        #endregion

        #region Implementation of IHomeService

        /// <summary>
        ///     All main models and setting models that the current User is allowed to see.
        /// </summary>
        /// <returns>The ViewModel for the home</returns>
        public HomeViewModel GetHomeViewModel()
        {
            
            var roleNames = RolesForUser;

            var vm = new HomeViewModel
            {
                Modules = HomeBll.AllVisibleModulesForRoles( roleNames )
            };
            return vm;
        }

        #endregion
    }
}