using EpamHomeTask.Business.AplicationInterface;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.Business
{
    public class InsightPageContext
    {
        private InsightPage page = new();

        public void SlideSetAmountOfElements(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                BrowserHelper.WaitCondition(() => page.ActiveElementNextButton.Enabled);
                NextElement();
                BrowserHelper.WaitCondition(() => page.NewActiveElement.GetAttribute("aria-hidden").Equals("false"));
            }

        }
        public void NextElement()
        {
            BrowserHelper.GetAction().MoveToElement(page.ActiveElementNextButton);
            page.ActiveElementNextButton.Click();
        }
        public void ClickActiveReadMoreButton()
        {
            BrowserHelper.GetAction().MoveToElement(page.ActiveElementReadMore);
            BrowserHelper.WaitCondition(() => page.ActiveElementReadMore.Displayed);
            page.ActiveElementReadMore.Click();
        }
        public string SaveActiveElementTitle() => page.ActiveElementTitle.Text;
        public void ScrollToArticleTitle() => BrowserHelper.GetAction().ScrollToElement(page.ArticleTitle); 
        public string GetArticleTitle() => page.ArticleTitle.Text;
    }
}
