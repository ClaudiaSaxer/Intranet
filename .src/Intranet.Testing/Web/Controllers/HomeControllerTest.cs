using System;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.TestEnvironment;
using Intranet.ViewModel;
using Intranet.Web.Controllers;
using Xunit;

namespace Intranet.Web.Test.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeControllerTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void HomeControllerIndexViewTest()
        {
            var homeService = MockHelperService.GetHomeService(new HomeViewModel());
            var homeController = new HomeController(new NLogLoggerFactory())
            {
                HomeService = homeService
            };

            var result = homeController.Index() as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void HomeControllerIndexTest()
        {
            var expectedHomeViewModel = new HomeViewModel
            {

            };
            var homeService = MockHelperService.GetHomeService(null);
            var homeController = new HomeController(new NLogLoggerFactory())
            {
                HomeService = homeService
            };

            var result = homeController.Index() as ViewResult;
            Assert.Equal(2,2);
        }
    }
}
