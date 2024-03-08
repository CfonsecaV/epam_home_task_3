using EpamHomeTask.Core;
using OpenQA.Selenium;

namespace EpamHomeTask.Business.AplicationInterface
{
    public class AboutPage
    {
        private readonly By epamAtSectionLocator = By.XPath("//span[contains(text(), 'EPAM at')]");
        private readonly By downloadButtonLocator = By.CssSelector("a[href*='EPAM_Corporate']");

        public AboutPage() { }

        public IWebElement EpamAtSection => BrowserFactory.Driver.FindElement(epamAtSectionLocator);
        public IWebElement DownloadButton => BrowserFactory.Driver.FindElement(downloadButtonLocator);
    }
}
