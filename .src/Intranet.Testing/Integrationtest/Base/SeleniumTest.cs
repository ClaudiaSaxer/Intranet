using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Xunit;

namespace Intranet.Integrationtest.Base
{
    /// <summary>
    ///     Base Class for Selenium Tests
    /// </summary>
    public abstract class SeleniumTest : IDisposable
    {
        private const String WebApplicationName = "Intranet.Web";
        private const Int32 IisPort = 29549;
        private Process _iisProcess;

        //protected FirefoxDriver FirefoxDriver { get; }
        /// <summary>
        /// 
        /// </summary>
        protected InternetExplorerDriver InternetExplorerDriver { get; }

        /// <summary>
        ///     Starts the IIS
        /// </summary>
        protected SeleniumTest()
        {
            // Start IISExpress
            StartIIS();

            // Start Selenium Drivers
            //FirefoxDriver = new FirefoxDriver("C:\\Users\\fjordi\\Intranet\\.tools\\Selenium");
            var seleniumToolsPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))))) + "\\.tools\\Selenium";
            InternetExplorerDriver = new InternetExplorerDriver(seleniumToolsPath);
        }

        /// <summary>
        ///     shutdown all process
        /// </summary>
        public void Dispose()
        {
            // Ensure IISExpress is stopped
            if (!_iisProcess.HasExited)
                _iisProcess.Kill();

            // End Selenium Drivers
            //FirefoxDriver.Quit();
            InternetExplorerDriver.Quit();
        }


        /// <summary>
        ///     
        /// </summary>
        private void StartIIS()
        {
            var configPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)))) + "\\.vs\\config\\applicationhost.config";
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programFiles + "\\IIS Express\\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = String.Format("/config:{0} /site:{1}", configPath, WebApplicationName);
            _iisProcess.StartInfo.UseShellExecute = true;
            _iisProcess.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        protected static String GetAbsoluteUrl(String relativeUrl)
        {
            if (!relativeUrl.StartsWith("/", StringComparison.Ordinal))
                relativeUrl = "/" + relativeUrl;
            return String.Format("http://localhost:{0}{1}", IisPort, relativeUrl);
        }
    }
}
