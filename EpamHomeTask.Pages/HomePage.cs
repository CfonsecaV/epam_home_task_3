using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Security.AccessControl;

namespace EpamHomeTask.Pages
{
    public class HomePage : IWaitable
    {
        private IWebDriver driver;

        private readonly By acceptCookiesButtonLocator = By.XPath("//button[contains(text(), 'Accept')]");
        private readonly By careersTopMenuLocator = By.XPath("//a[@class='top-navigation__item-link js-op'][.='Careers']");
        private readonly By searchIconLocator = By.CssSelector("span.search-icon");
        private readonly By searchTextBoxLocator = By.Name("q");
        private readonly By mainFindButtonLocator = By.XPath("//span[contains(text(), 'Find')]");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement GetCareersTopMenu() { return driver.FindElement(careersTopMenuLocator); }
        public IWebElement GetAcceptCookiesButton() { return driver.FindElement(acceptCookiesButtonLocator); }
        public IWebElement GetSearchIcon() { return driver.FindElement(searchIconLocator); }
        public IWebElement GetSearchTextBox() { return driver.FindElement(searchTextBoxLocator); }
        public IWebElement GetMainFindButton() { return driver.FindElement(mainFindButtonLocator); }

        public void ClickSearchIcon()
        {
            GetSearchIcon().Click();
        }

        public void InputSearchKeyword(string searchItem)
        {
            GetSearchTextBox().SendKeys(searchItem);
        }

        public SearchResultPage ClickMainFindButton()
        {
            GetMainFindButton().Click();
            return new SearchResultPage(driver);
        }

        public CareersSearchPage ClickCareerButton()
        {
            GetCareersTopMenu().Click();
            return new CareersSearchPage(driver);
        }

        public void AcceptCookies(WebDriverWait wait)
        {
            var condition1 = wait.Until(d => d.FindElement(acceptCookiesButtonLocator));
            var condition2 = wait.Until(d => d.FindElement(acceptCookiesButtonLocator).Enabled);
            GetAcceptCookiesButton().Click();
        }

        public bool WaitDisplayed(WebDriverWait wait, IWebElement element)
        {
            return wait.Until(d => element.Displayed);         
        }
    }
}
