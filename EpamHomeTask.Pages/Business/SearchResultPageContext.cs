using EpamHomeTask.Business.AplicationInterface;
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
        SearchResultPage page = new();

        public List<IWebElement> GetListElements()
        {
            return page.ListElements;
        }
        public void ScrollToFooter() => BrowserHelper.GetAction().ScrollToElement(page.Footer).Perform();
        public List<IWebElement> FilterList(string keyword)
        {
            var filteredElements = page.ListElements
                        .Where(element => element.GetAttribute("href")
                        .Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        .ToList();
            return filteredElements;
        }
    }
}
