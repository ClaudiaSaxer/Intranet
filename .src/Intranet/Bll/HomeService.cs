using System.Linq;
using System.Web.Security;
using Intranet.Definition;
using Intranet.ViewModel;

namespace Intranet.Bll
{
    /// <summary>
    /// </summary>
    public class HomeService : LoggingBase, IHomeService
    {
        #region Properties

        /// <summary>
        /// </summary>
        public ITestBll Bll { get; set; }

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
        /// </summary>
        /// <returns></returns>
        public HomeViewModel GetHomeViewModel()
        {
			Logger.Trace( "Enter GetTestViewModel - Exit" );

            var roleNames = Roles.GetRolesForUser();
            var result = new HomeViewModel

            {
              /*  Name = Bll.AllTests()
                          .FirstOrDefault()
                          ?.TestString
                          Name = "this and so" */
            };

            return result;
        }

        #endregion
    }
}