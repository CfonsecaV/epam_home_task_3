using EpamHomeTask.Core;
using OpenQA.Selenium;

namespace EpamHomeTask.Business.Pages
{
    public class AboutPage
    {
        private readonly IWebDriver _webDriver;

        private readonly By epamAtSectionLocator = By.XPath("//span[contains(text(), 'EPAM at')]");
        private readonly By downloadButtonLocator = By.CssSelector("a[href*='EPAM_Corporate']");

        public AboutPage(IWebDriver driver) 
        {
            _webDriver = driver;
        }
        public IWebDriver Driver => _webDriver;
        public IWebElement EpamAtSection => _webDriver.FindElement(epamAtSectionLocator);
        public IWebElement DownloadButton => _webDriver.FindElement(downloadButtonLocator);
    }
}
