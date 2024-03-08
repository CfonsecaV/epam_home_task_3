using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace EpamHomeTask.Core
{
    public class BrowserFactory
    {
        private static readonly IDictionary<string, IWebDriver> Drivers = new Dictionary<string, IWebDriver>();
        private static IWebDriver? driver;

        public static IWebDriver Driver
        {
            get {
                if (driver == null) 
                {
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. " +
                        "You should first call the method InitBrowser.");
                }
                return driver;
            }
            private set { driver = value; }
        }
        
        public static void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    if(Driver == null)
                    {
                        FirefoxOptions options = new();
                        options.AddArgument("--start-maximized");
                        driver = new FirefoxDriver(options);
                        Drivers.Add("Firefox", driver);
                    }
                    break;

                case "Chrome":
                    if (Driver == null)
                    {
                        ChromeOptions options = new();
                        options.AddArgument("--start-maximized");
                        driver = new ChromeDriver(options);
                        Drivers.Add("Chrome", driver);
                    }
                    break;

                case "Edge":
                    if (Driver == null)
                    {
                        EdgeOptions options = new();
                        options.AddArgument("--start-maximized");
                        driver = new EdgeDriver(options);
                        Drivers.Add("Edge", driver);
                    }
                    break;
            }
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

        public static void CloseAllDrivers()
        {
            foreach (var key in Drivers.Keys) 
            {
                Drivers[key].Close();
                Drivers[key].Quit();
            }
        }
    }
}
