using EpamHomeTask.Business.AplicationInterface;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.Business
{
    public class HomePageContext
    {
        private HomePage page = new();

        public static HomePageContext Open()
        {
            BrowserFactory.Driver.Navigate().GoToUrl(BrowserFactory.LoadApplication());
            return new HomePageContext();
        }
        public void ClickSearchIcon() => page.SearchIcon.Click();
        public void InputSearchKeyword(string searchItem)
        {
            BrowserHelper.WaitCondition(() => page.SearchTextBox.Displayed);
            page.SearchTextBox.SendKeys(searchItem);
        }
        public SearchResultPageContext ClickMainFindButton()
        {
            page.MainFindButton.Click();
            return new SearchResultPageContext();
        }
        public CareerSearchPageContext ClickCareerButton()
        {
            page.CareersTopMenu.Click();
            return new CareerSearchPageContext();
        }
        public AboutPageContext ClickAboutButton()
        {
            page.AboutTopMenu.Click();
            return new AboutPageContext();
        }
        public InsightPageContext ClickInsightButton()
        {
            page.InsightTopMenu.Click();
            return new InsightPageContext();
        }
        public void AcceptCookies()
        {
            BrowserHelper.WaitCondition(() => page.AcceptCookiesButton.Enabled && page.AcceptCookiesButton.Displayed);
            page.AcceptCookiesButton.Click();
        }
    }
}
