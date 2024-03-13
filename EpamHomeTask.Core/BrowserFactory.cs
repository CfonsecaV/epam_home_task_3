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
        public BrowserFactory(Browsers browser)
        {
            InitBrowser(browser);
        }

        public IWebDriver InitBrowser(Browsers browser)
        {
            var webDriver = browser switch
            {
                Browsers.Chrome => InitChromeDriver(),
                Browsers.Firefox => InitFirefoxDriver(),
                Browsers.Edge => InitEdgeDriver(),
                _ => throw new ArgumentOutOfRangeException(nameof(browser))
            };
            return webDriver;
        }
        public IWebDriver InitChromeDriver()
        {
            ChromeOptions options = new();
            options.AddArgument("--start-maximized");
            var webdriver = new ChromeDriver(options);
            return webdriver;
        }
        public IWebDriver InitFirefoxDriver()
        {
            FirefoxOptions options = new();
            options.AddArgument("--start-maximized");
            var webdriver = new FirefoxDriver(options);
            return webdriver;
        }
        public IWebDriver InitEdgeDriver()
        {
            EdgeOptions options = new();
            options.AddArgument("--start-maximized");
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
