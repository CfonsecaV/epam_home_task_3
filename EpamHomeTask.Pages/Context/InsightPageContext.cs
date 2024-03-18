using EpamHomeTask.Business.Pages;
using EpamHomeTask.Core;
using OpenQA.Selenium;

namespace EpamHomeTask.Business.Context
{
    public class InsightPageContext
    {
        private InsightPage _page;

        public InsightPageContext(IWebDriver webDriver)
        {
            _page = new InsightPage(webDriver);
        }
        public void SlideSetAmountOfElements(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                BrowserHelper.WaitCondition(_page.Driver, () => _page.ActiveElementNextButton.Enabled);
                NextElement();
                BrowserHelper.WaitCondition(_page.Driver, () => _page.NewActiveElement.GetAttribute("aria-hidden").Equals("false"));
            }

        }
        public void NextElement()
        {
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.ActiveElementNextButton);
            _page.ActiveElementNextButton.Click();
        }
        public void ClickActiveReadMoreButton()
        {
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.ActiveElementReadMore);
            BrowserHelper.WaitCondition(_page.Driver, () => _page.ActiveElementReadMore.Displayed);
            _page.ActiveElementReadMore.Click();
        }
        public string SaveActiveElementTitle() => _page.ActiveElementTitle.Text;
        public void ScrollToArticleTitle() => BrowserHelper.GetAction(_page.Driver).ScrollToElement(_page.ArticleTitle); 
        public string GetArticleTitle() => _page.ArticleTitle.Text;
    }
}
