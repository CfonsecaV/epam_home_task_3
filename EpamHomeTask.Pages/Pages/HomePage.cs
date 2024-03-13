using OpenQA.Selenium;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _webDriver;
        private readonly string _baseUrl;

        private readonly By acceptCookiesButtonLocator = By.XPath("//button[contains(text(), 'Accept')]");
        private readonly By careersTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='Careers']");
        private readonly By aboutTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='About']");
        private readonly By insightTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='Insights']");
        private readonly By searchIconLocator = By.CssSelector("span.search-icon");
        private readonly By searchTextBoxLocator = By.Name("q");
        private readonly By mainFindButtonLocator = By.XPath("//span[contains(text(), 'Find')]");

        public HomePage(IWebDriver webDriver) 
        {
            _webDriver = webDriver;
            _baseUrl = BrowserFactory.LoadApplication();
        }
        public string BaseUrl => _baseUrl;
        public IWebDriver Driver => _webDriver;
        public IWebElement CareersTopMenu => _webDriver.FindElement(careersTopMenuLocator);
        public IWebElement AcceptCookiesButton => _webDriver.FindElement(acceptCookiesButtonLocator);
        public IWebElement SearchIcon => _webDriver.FindElement(searchIconLocator);
        public IWebElement SearchTextBox => _webDriver.FindElement(searchTextBoxLocator);
        public IWebElement MainFindButton => _webDriver.FindElement(mainFindButtonLocator);
        public IWebElement AboutTopMenu => _webDriver.FindElement(aboutTopMenuLocator);
        public IWebElement InsightTopMenu => _webDriver.FindElement(insightTopMenuLocator);      
    }
}
