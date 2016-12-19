#region Usings

using Intranet.Common;
using Intranet.Definition;
using Intranet.ViewModel;

#endregion

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
        public HomeService( ILoggerFactory loggerFactory )
            : base( loggerFactory.CreateLogger( typeof(HomeService) ) )
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
            var roleNames = Roles.GetRolesForUser();

            var vm = new HomeViewModel
            {
                Modules = HomeBll.AllVisibleModulesForRoles( roleNames )
            };
            return vm;
        }

        #endregion
    }
}