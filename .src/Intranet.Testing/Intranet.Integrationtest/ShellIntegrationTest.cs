using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Integrationtest.Base;
using OpenQA.Selenium;
using Xunit;

namespace Intranet.Integrationtest
{
    /// <summary>
    ///     A test class for integration tests
    /// </summary>
    public class ShellIntegrationTest : SeleniumTest
    {
        [Fact(Skip = "Integrationstest with IIS Doesnt Work ATM")]
        [Trait("Integration Test", "Shell")]
        public void CountOfModulesTest()
        {
            InternetExplorerDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
            var countOfModules = InternetExplorerDriver.FindElementsByClassName( "modul-item" )
                                                       .Count;
            // Assert
            Assert.Equal(2, countOfModules);
        }

        [Fact(Skip = "Integrationstest with IIS Doesnt Work ATM")]
        [Trait("Integration Test","Shell")]
        public void LinkToSettingsTest()
        {
            InternetExplorerDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
            InternetExplorerDriver.FindElementById( "Einstellungen" ).Click();
            // Assert
            Assert.Equal(GetAbsoluteUrl("/Settings"), InternetExplorerDriver.Url);
        }
    }
}
