using System.Web.Security;
using Intranet.Definition;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    /// <summary>
    ///     TODO
    /// </summary>
    public class HomeService : LoggingBase, IHomeService
    {
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
        ///     TODO
        /// </summary>
        /// <returns></returns>
        public HomeViewModel GetHomeViewModel()
        {
            //TODO implement
            Logger.Trace( "Enter GetTestViewModel - Exit" );

            var roleNames = Roles.GetRolesForUser();
            var result = new HomeViewModel();

            return result;
        }

        #endregion
    }
}