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
    public class ShellIntegrationTest : SeleniumTest
    {
        [Fact]
        [Trait("Integration Test", "Shell")]
        public void CountOfModulesTest()
        {
            InternetExplorerDriver.Navigate().GoToUrl(GetAbsoluteUrl("/"));
            var countOfModules = InternetExplorerDriver.FindElementsByClassName( "modul-item" )
                                                       .Count;
            // Assert
            Assert.Equal(2, countOfModules);
        }

        [Fact]
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
