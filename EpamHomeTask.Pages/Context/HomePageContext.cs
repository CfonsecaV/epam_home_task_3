using EpamHomeTask.Business.Pages;
using EpamHomeTask.Core;
using OpenQA.Selenium;

namespace EpamHomeTask.Business.Context
{
    public class HomePageContext
    {
        private HomePage _page;

        public HomePageContext(IWebDriver webDriver)
        {
            _page = new HomePage(webDriver);
        }

        public void Open() => _page.Driver.Navigate().GoToUrl(_page.BaseUrl); 
        public void ClickSearchIcon() => _page.SearchIcon.Click();
        public void InputSearchKeyword(string searchItem)
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.SearchTextBox.Displayed);
            _page.SearchTextBox.SendKeys(searchItem);
        }
        public SearchResultPageContext ClickMainFindButton()
        {
            _page.MainFindButton.Click();
            return new SearchResultPageContext(_page.Driver);
        }
        public CareerSearchPageContext ClickCareerButton()
        {
            _page.CareersTopMenu.Click();
            return new CareerSearchPageContext(_page.Driver);
        }
        public AboutPageContext ClickAboutButton()
        {
            _page.AboutTopMenu.Click();
            return new AboutPageContext(_page.Driver);
        }
        public InsightPageContext ClickInsightButton()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.InsightTopMenu.Enabled);
            _page.InsightTopMenu.Click();
            return new InsightPageContext(_page.Driver);
        }
        public void AcceptCookies()
        {
            BrowserHelper.WaitCondition(_page.Driver,() => _page.AcceptCookiesButton.Enabled && _page.AcceptCookiesButton.Displayed);
            _page.AcceptCookiesButton.Click();
        }
    }
}
