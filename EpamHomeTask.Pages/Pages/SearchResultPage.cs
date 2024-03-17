using OpenQA.Selenium;

namespace EpamHomeTask.Business.Pages
{
    public class SearchResultPage
    {
        private readonly IWebDriver _webDriver;

        private readonly By listElementLocator = By.CssSelector(".search-results__title-link");
        private readonly By footerLocator = By.CssSelector("footer.search-results__footer");

        public SearchResultPage(IWebDriver webDriver) 
        {
            _webDriver = webDriver;
        }
        public IWebDriver Driver => _webDriver;
        public IWebElement Footer => _webDriver.FindElement(footerLocator);
        public List<IWebElement> ListElements => _webDriver.FindElements(listElementLocator).ToList();        
    }
}
