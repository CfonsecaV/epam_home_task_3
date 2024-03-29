using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace EpamHomeTask.Core
{
    public class BrowserFactory
    {
        private readonly IWebDriver? _webDriver;
        public BrowserFactory(Browsers browser, string downloadPath = "")
        {
            _webDriver = InitBrowser(browser, downloadPath);
        }

        public IWebDriver InitBrowser(Browsers browser, string downloadPath)
        {
            var webDriver = browser switch
            {
                Browsers.Chrome => InitChromeDriver(downloadPath),
                Browsers.Firefox => InitFirefoxDriver(downloadPath),
                Browsers.Edge => InitEdgeDriver(downloadPath),
                _ => throw new ArgumentOutOfRangeException(nameof(browser))
            };
            return webDriver;
        }
        public IWebDriver InitChromeDriver(string downloadPath = "")
        {
            ChromeOptions options = new();
            if (!string.IsNullOrEmpty(downloadPath))
            {
                options.AddUserProfilePreference("download.default_directory", downloadPath);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("download.directory_upgrade", true);
                options.AddUserProfilePreference("safebrowsing.enabled", true);
            }
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--headless");
            var webdriver = new ChromeDriver(options);
            return webdriver;
        }
        public IWebDriver InitFirefoxDriver(string downloadPath = "")
        {
            FirefoxOptions options = new();
            if (!string.IsNullOrEmpty(downloadPath))
            {
                FirefoxProfile profile = new();
                profile.SetPreference("browser.download.dir", downloadPath);
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.useDownloadDir", true);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk",
                        "application/pdf,text/plain,application/octet-stream");
                profile.SetPreference("pdfjs.disabled", false);
                profile.SetPreference("browser.helperApps.alwaysAsk.force", false);
                options.Profile = profile;

            }
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");
            options.AddArgument("--headless");            
            var webdriver = new FirefoxDriver(options);
            return webdriver;
        }
        public IWebDriver InitEdgeDriver(string downloadPath = "")
        {
            EdgeOptions options = new();            
            if (!string.IsNullOrEmpty(downloadPath))
            {
                options.AddUserProfilePreference("download.default_directory", downloadPath);
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("download.directory_upgrade", true);
                options.AddUserProfilePreference("safebrowsing.enabled", true);
            }
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--headless");
            var webDriver = new EdgeDriver(options);
            return webDriver;
        }
        public IWebDriver GetInstanceOf()
        {
            if(_webDriver == null)
            {
                throw new InvalidOperationException("WebDriver hasn't been initialized");
            }
            return _webDriver;
        } 
        public static string LoadApplication()
        {   
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json");
            var configuration = configBuilder.Build();
            string baseUrl = configuration["BaseUrl"] ?? string.Empty;
            return baseUrl;
        }
        public static void CloseAllDrivers(IWebDriver webDriver)
        {
            if (webDriver == null)
            {
                throw new InvalidOperationException("WebDriver hasn't been initialized");
            }
            webDriver.Close();
            webDriver.Quit();
            webDriver.Dispose();
        }
    }
}
