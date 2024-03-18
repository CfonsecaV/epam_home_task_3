using EpamHomeTask.Business.Pages;
using EpamHomeTask.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V120.Browser;
using OpenQA.Selenium.Interactions;

namespace EpamHomeTask.Business.Context
{
    public class SpecflowDocsPageContext
    {
        private SpecflowDocsPage _page;

        public SpecflowDocsPageContext(IWebDriver webDriver)
        {
            _page = new SpecflowDocsPage(webDriver);
        }
        public void SelectSearchBar()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.SearchBar.Enabled);
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.SearchBar).Perform();
            _page.SearchBar.Click();
        }
        public void InputKeyword(string keyword)
        {
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.SearchPopUp);
            _page.SearchPopUp.SendKeys(keyword);
        }
        public void ClickSearchResult()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.SearchResult.Enabled);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_page.Driver;
            jse.ExecuteScript("arguments[0].click();", _page.SearchResult);
        }
        public string GetResultTitle()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.SearchResultTitle.Displayed);            
            return _page.SearchResultTitle.Text;
        }
    }
}
