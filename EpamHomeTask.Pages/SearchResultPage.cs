using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Pages
{
    public class SearchResultPage 
    {
        private IWebDriver driver;

        private readonly By listElementLocator = By.CssSelector(".search-results__title-link");
        private readonly By footerLocator = By.CssSelector("footer.search-results__footer");

        public SearchResultPage(IWebDriver driver) {
            this.driver = driver;
        }

        public IWebElement GetFooter() { return driver.FindElement(footerLocator); }
        public List<IWebElement> GetListElements() { return [.. driver.FindElements(listElementLocator)]; }

        public void ScrollToFooter(IWebDriver driver)
        {   
            Actions action = new(driver);
            action.ScrollToElement(GetFooter()).Perform();
        }        

        public List<IWebElement> FilterList(string keyword)
        {
            var filteredElements = GetListElements()
                        .Where(element => element.GetAttribute("href")
                        .Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        .ToList();
            return filteredElements;
        }
    }
}
