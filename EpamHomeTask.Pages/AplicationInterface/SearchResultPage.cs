using EpamHomeTask.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Business.AplicationInterface
{
    public class SearchResultPage
    {
        private readonly By listElementLocator = By.CssSelector(".search-results__title-link");
        private readonly By footerLocator = By.CssSelector("footer.search-results__footer");

        public SearchResultPage() { }

        public IWebElement Footer => BrowserFactory.Driver.FindElement(footerLocator);
        public List<IWebElement> ListElements => [.. BrowserFactory.Driver.FindElements(listElementLocator)];        
    }
}
