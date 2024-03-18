using EpamHomeTask.Business.Pages;
using EpamHomeTask.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace EpamHomeTask.Business.Context
{
    public class SpecflowHomePageContext
    {
        private SpecflowHomePage _page;

        public SpecflowHomePageContext(IWebDriver webDriver)
        {
            _page = new SpecflowHomePage(webDriver);
        }
        public void Open() => _page.Driver.Navigate().GoToUrl(_page.SpecflowUrl);

        public void AcceptCookies()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.AcceptCookies.Displayed);
            _page.AcceptCookies.Click();
        }
        public void MoveToDocsTopMenu()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.DocsTopMenu.Enabled);
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.DocsTopMenu).Perform();
        }
        public SpecflowDocsPageContext SelectSpecflowDocumentation()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.SubMenu.Displayed);
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.SpecflowDocumentation);
            _page.SpecflowDocumentation.Click();
            return new SpecflowDocsPageContext(_page.Driver);
        }
    }
}
