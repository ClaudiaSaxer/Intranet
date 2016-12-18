#region Usings

using System.Collections.Generic;
using System.Web.Mvc;
using Intranet.Common;
using Intranet.TestEnvironment;
using Intranet.ViewModel;
using Intranet.Web.Controllers;
using Xunit;

#endregion

namespace Intranet.Web.Test
{
    /// <summary>
    ///     A test class for the settings controller
    /// </summary>
    public class SettingsControllerTest
    {
        /// <summary>
        ///     Tests if the View has the right ViewModel
        /// </summary>
        [Fact]
        public void HomeControllerIndexTest()
        {
            var expectedSettingsViewModel = new SettingsViewModel
            {
                ModuleSettings = new List<ModuleSetting>
                {
                    new ModuleSetting { Id = 1, Name = "Labor", Visible = true },
                    new ModuleSetting { Id = 2, Name = "Einstellungen", Visible = true }
                }
            };
            var settingsService = MockHelperService.GetSettingsService( expectedSettingsViewModel );
            var settingsController = new SettingsController( new NLogLoggerFactory() )
            {
                SettingsService = settingsService
            };

            var result = settingsController.Index() as ViewResult;
            var settingsViewModel = (SettingsViewModel) result?.ViewData.Model;
            Assert.Equal( expectedSettingsViewModel, settingsViewModel );
        }

        /// <summary>
        ///     Tests if the returned view is the right view
        /// </summary>
        [Fact]
        public void SettingsControllerIndexViewTest()
        {
            var settingsService = MockHelperService.GetSettingsService( new SettingsViewModel() );
            var settingsController = new SettingsController( new NLogLoggerFactory() )
            {
                SettingsService = settingsService
            };

            var result = settingsController.Index() as ViewResult;
            Assert.Equal( "Index", result?.ViewName );
        }

        /// <summary>
        ///     Tests if the Update Action doesnt throw an exception if input correct
        ///     Should return a RedirectToRouteResult
        /// </summary>
        [Fact]
        public void SettingsControllerUpdateDontThrowTest()
        {
            var settingsService = MockHelperService.GetSettingsService( null );
            var settingsController = new SettingsController( new NLogLoggerFactory() )
            {
                SettingsService = settingsService
            };
            var result = settingsController.Update( new ModuleSetting() );
            Assert.Equal( "RedirectToRouteResult",
                          result.GetType()
                                .Name );
        }

        /// <summary>
        ///     Tests if the Update Action couldnt update cause an exception
        ///     Should return a fault ViewResult
        /// </summary>
        [Fact]
        public void SettingsControllerUpdateFailTest()
        {
            var settingsService = MockHelperService.GetSettingsService( null );
            var settingsController = new SettingsController( new NLogLoggerFactory() )
            {
                SettingsService = settingsService
            };
            var result = settingsController.Update( null );
            Assert.IsType( typeof(HttpNotFoundResult), result );
        }
    }
}