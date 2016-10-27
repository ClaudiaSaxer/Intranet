using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Xunit;

namespace Intranet.Integrationtest.Base
{
    public abstract class SeleniumTest : IDisposable
    {
        private String _webApplicationName = "Intranet.Web";
        private Int32 _iisPort = 29549;
        private Process _iisProcess;

        public FirefoxDriver FirefoxDriver { get; set; }
        public InternetExplorerDriver InternetExplorerDriver { get; set; }

        protected SeleniumTest()
        {
            // Start IISExpress
            StartIIS();

            // Start Selenium Drivers
            //FirefoxDriver = new FirefoxDriver("C:\\Users\\fjordi\\Intranet\\.tools\\Selenium");
            InternetExplorerDriver = new InternetExplorerDriver("C:\\Users\\fjordi\\Intranet\\.tools\\Selenium");
        }

        public void Dispose()
        {
            // Ensure IISExpress is stopped
            if (!_iisProcess.HasExited)
                _iisProcess.Kill();

            // End Selenium Drivers
            //FirefoxDriver.Quit();
            InternetExplorerDriver.Quit();
        }



        private void StartIIS()
        {
            var configPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)))) + "\\.vs\\config\\applicationhost.config";
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programFiles + "\\IIS Express\\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = String.Format("/config:{0} /site:{1}", configPath, _webApplicationName);
            _iisProcess.StartInfo.UseShellExecute = true;
            _iisProcess.Start();
        }

        public String GetAbsoluteUrl(String relativeUrl)
        {
            if (!relativeUrl.StartsWith("/", StringComparison.Ordinal))
                relativeUrl = "/" + relativeUrl;
            return String.Format("http://localhost:{0}{1}", _iisPort, relativeUrl);
        }
    }
}
