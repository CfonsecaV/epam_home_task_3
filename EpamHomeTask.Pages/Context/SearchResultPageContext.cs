using EpamHomeTask.Business.Pages;
using EpamHomeTask.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Business.Business
{
    public class SearchResultPageContext
    {
        SearchResultPage _page;

        public SearchResultPageContext(IWebDriver webDriver)
        {
            _page = new SearchResultPage(webDriver);
        }

        public List<IWebElement> GetListElements()
        {
            return _page.ListElements;
        }
        public void ScrollToFooter() => BrowserHelper.GetAction(_page.Driver).ScrollToElement(_page.Footer).Perform();
        public List<IWebElement> FilterList(string keyword)
        {
            var filteredElements = _page.ListElements
                        .Where(element => element.GetAttribute("href")
                        .Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        .ToList();
            return filteredElements;
        }
    }
}
