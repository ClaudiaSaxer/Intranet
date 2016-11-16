using System.Collections.Generic;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.Model;
using Intranet.TestEnvironment;
using Intranet.ViewModel;
using Intranet.Web.Controllers;
using Xunit;

namespace Intranet.Web.Test.Controllers
{
    /// <summary>
    ///     A test class for the home controller
    /// </summary>
    public class HomeControllerTest
    {
        /// <summary>
        ///     Tests if the View has the right Model
        /// </summary>
        [Fact]
        public void HomeControllerIndexTest()
        {
            var expectedHomeViewModel = new HomeViewModel
            {
                Modules = new List<Module>
                {
                    new Module { Name = "Labor", Description = "Labor QS", ActionName = "Index", ControllerName = "LaborHome", Visible = true, AreaName = "Labor" },
                    new Module
                    {
                        Name = "Einstellungen",
                        Description = "Einstellungen für die Shell",
                        ActionName = "Index",
                        ControllerName = "Settings",
                        Visible = true,
                        AreaName = ""
                    }
                }
            };
            var homeService = MockHelperService.GetHomeService( expectedHomeViewModel );
            var homeController = new HomeController( new NLogLoggerFactory() )
            {
                HomeService = homeService
            };

            var result = homeController.Index() as ViewResult;
            var homeViewModel = (HomeViewModel) result?.ViewData.Model;
            Assert.Equal(expectedHomeViewModel,homeViewModel);
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void HomeControllerIndexViewTest()
        {
            var homeService = MockHelperService.GetHomeService( new HomeViewModel() );
            var homeController = new HomeController( new NLogLoggerFactory() )
            {
                HomeService = homeService
            };

            var result = homeController.Index() as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }
    }
}