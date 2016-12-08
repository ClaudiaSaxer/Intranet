using Xunit;

namespace Intranet.Integrationtest
{
    /// <summary>
    ///     A test class for integration tests
    /// </summary>
    public class ShellIntegrationTest : SeleniumTest
    {
        /// <summary>
        /// </summary>
        [Fact( Skip = "Integrationstest with IIS Doesnt Work ATM" )]
        [Trait( "Integration Test", "Shell" )]
        public void CountOfModulesTest()
        {
            InternetExplorerDriver.Navigate()
                                  .GoToUrl( GetAbsoluteUrl( "/" ) );
            var countOfModules = InternetExplorerDriver.FindElementsByClassName( "modul-item" )
                                                       .Count;
            // Assert
            Assert.Equal( 2, countOfModules );
        }

        /// <summary>
        /// </summary>
        [Fact( Skip = "Integrationstest with IIS Doesnt Work ATM" )]
        [Trait( "Integration Test", "Shell" )]
        public void LinkToSettingsTest()
        {
            InternetExplorerDriver.Navigate()
                                  .GoToUrl( GetAbsoluteUrl( "/" ) );
            InternetExplorerDriver.FindElementById( "Einstellungen" )
                                  .Click();
            // Assert
            Assert.Equal( GetAbsoluteUrl( "/Settings" ), InternetExplorerDriver.Url );
        }
    }
}