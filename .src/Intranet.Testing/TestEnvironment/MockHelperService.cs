using Intranet.Definition;
using Intranet.Model;
using Intranet.ViewModel;
using Moq;

namespace Intranet.TestEnvironment
{
    /// <summary>
    ///     A mock helper class for Services
    /// </summary>
    public static class MockHelperService
    {
        /// <summary>
        ///     The Mock for the HomeService
        /// </summary>
        public static IHomeService GetHomeService( HomeViewModel homeViewModel )
        {
            var mock = new Mock<IHomeService>
            {
                Name = "MockHelperService.GetHomeService",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( homeService => homeService.GetHomeViewModel() )
                .Returns( homeViewModel );

            return mock.Object;
        }

        /// <summary>
        ///     The Mock for the SettingsService
        /// </summary>
        public static ISettingsService GetSettingsService( SettingsViewModel settingsViewModel )
        {
            var mock = new Mock<ISettingsService>
            {
                Name = "MockHelperService.GetSettingService",
                DefaultValue = DefaultValue.Mock
            };

            mock.Setup( settingsService => settingsService.GetSettingsViewModel() )
                .Returns( settingsViewModel );

            mock.Setup( settingsService => settingsService.UpdateModuleSetting( It.IsNotNull<ModuleSetting>() ) )
                .Returns( new Module() );
            Module nullModule = null;
            mock.Setup( settingsService => settingsService.UpdateModuleSetting( null ) )
                .Returns( nullModule );

            return mock.Object;
        }
    }
}