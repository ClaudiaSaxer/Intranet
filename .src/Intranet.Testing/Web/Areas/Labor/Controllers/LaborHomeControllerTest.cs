#region Usings

using System.Collections.Generic;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.Labor.ViewModel;
using Intranet.Model;
using Intranet.TestEnvironment;
using Intranet.Web.Areas.Labor.Controllers;
using Xunit;

#endregion

namespace Intranet.Web.Test
{
    /// <summary>
    ///     A test class for the labor home controller
    /// </summary>
    public class LaborHomeControllerTest
    {
        /// <summary>
        ///     Tests if the View has the right Model
        /// </summary>
        [Fact]
        public void HomeControllerIndexTest()
        {
            var expectedHomeViewModel = new LaborHomeViewModel
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
            var laborHomeService = MockHelperLaborControllerService.GetLaborHomeService( expectedHomeViewModel );
            var laborHomeController = new LaborHomeController( new NLogLoggerFactory() )
            {
                LaborHomeService = laborHomeService
            };

            var result = laborHomeController.Index() as ViewResult;
            var laborHomeViewModel = (LaborHomeViewModel) result?.ViewData.Model;
            Assert.Equal( expectedHomeViewModel, laborHomeViewModel );
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void HomeControllerIndexViewTest()
        {
            var laborHomeService = MockHelperLaborControllerService.GetLaborHomeService( new LaborHomeViewModel() );
            var laborHomeController = new LaborHomeController( new NLogLoggerFactory() )
            {
                LaborHomeService = laborHomeService
            };

            var result = laborHomeController.Index() as ViewResult;
            Assert.Equal( "Index", result?.ViewName );
        }
    }
}