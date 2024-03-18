using OpenQA.Selenium;

namespace EpamHomeTask.Business.Pages
{
    public class SpecflowHomePage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _specflowUrl;

        private readonly By acceptCookiesLocator = By.Id("consent-accept");
        private readonly By docsTopMenuLocator = By.XPath("//*[.=\"Docs\"]//span");
        private readonly By subMenuLocator = By.CssSelector("#menu-item-1064 .sub-menu-ux-wrap");
        private readonly By specflowDocumentationLocator = By.CssSelector("a[href='https://docs.specflow.org/projects/specflow/en/latest/']");
        public SpecflowHomePage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _specflowUrl = "https://specflow.org/";
        }

        public IWebDriver Driver => _webDriver;
        public string SpecflowUrl => _specflowUrl;
        public IWebElement AcceptCookies => _webDriver.FindElement(acceptCookiesLocator);
        public IWebElement DocsTopMenu => _webDriver.FindElement(docsTopMenuLocator);
        public IWebElement SubMenu => _webDriver.FindElement(subMenuLocator);
        public IWebElement SpecflowDocumentation => SubMenu.FindElement(specflowDocumentationLocator);
    }
}
