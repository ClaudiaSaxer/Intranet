using System.Linq;
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
        public TestViewModel GetTestViewModel()
        {
            var result = new TestViewModel
            {
                Name = Bll.AllTests().First()
                          .TestString
            };

            return result;
        }

        #endregion
    }
}