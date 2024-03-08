using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using EpamHomeTask.Business.Business;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.AplicationInterface
{
    public class HomePage
    {
        private readonly By acceptCookiesButtonLocator = By.XPath("//button[contains(text(), 'Accept')]");
        private readonly By careersTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='Careers']");
        private readonly By aboutTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='About']");
        private readonly By insightTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='Insights']");
        private readonly By searchIconLocator = By.CssSelector("span.search-icon");
        private readonly By searchTextBoxLocator = By.Name("q");
        private readonly By mainFindButtonLocator = By.XPath("//span[contains(text(), 'Find')]");

        public HomePage() { }     

        public IWebElement CareersTopMenu => BrowserFactory.Driver.FindElement(careersTopMenuLocator);
        public IWebElement AcceptCookiesButton => BrowserFactory.Driver.FindElement(acceptCookiesButtonLocator);
        public IWebElement SearchIcon => BrowserFactory.Driver.FindElement(searchIconLocator);
        public IWebElement SearchTextBox => BrowserFactory.Driver.FindElement(searchTextBoxLocator);
        public IWebElement MainFindButton => BrowserFactory.Driver.FindElement(mainFindButtonLocator);
        public IWebElement AboutTopMenu => BrowserFactory.Driver.FindElement(aboutTopMenuLocator);
        public IWebElement InsightTopMenu => BrowserFactory.Driver.FindElement(insightTopMenuLocator);      
    }
}
