using OpenQA.Selenium;

namespace EpamHomeTask.Business.Pages
{
    public class SpecflowDocsPage
    {
        private readonly IWebDriver _webDriver;

        private readonly By searchBarLocator = By.CssSelector("input[placeholder='Search docs']");
        private readonly By searchPopUpLocator = By.CssSelector("input.search__outer__input");
        private readonly By searchResultLocator = By.CssSelector("a[href='/projects/specflow/en/latest/visualstudio/visual-studio-installation.html']");
        private readonly By searchResultTitle = By.CssSelector("h1");

        public SpecflowDocsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
        public IWebDriver Driver => _webDriver;
        public IWebElement SearchBar => _webDriver.FindElement(searchBarLocator);
        public IWebElement SearchPopUp => _webDriver.FindElement(searchPopUpLocator);
        public IWebElement SearchResult => _webDriver.FindElement(searchResultLocator);
        public IWebElement SearchResultTitle => _webDriver.FindElement(searchResultTitle);
    }
}
