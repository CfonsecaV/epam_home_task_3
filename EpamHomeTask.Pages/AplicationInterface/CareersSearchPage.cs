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
    public class CareersSearchPage
    {
        private readonly By languageTextBoxLocator = By.Id("new_form_job_search-keyword");
        private readonly By locationDropdownButtonLocator = By.CssSelector("span[role=presentation]");
        private readonly By locationDropDownMenuLocator = By.CssSelector("ul[role] .os-content");
        private readonly By remoteCheckboxLocator = By.CssSelector("[name=remote]+label");
        private readonly By selectedLocationLocator = By.CssSelector(".select2-selection__rendered");
        private readonly By findButtonLocator = By.XPath("//button[contains(text(), 'Find')]");
        private readonly By viewMoreButtonLocator = By.XPath("//a[.='View More']");
        private readonly By viewAndApplyButtonLocator = By.CssSelector("li.search-result__item:last-child .button-text");
        private string allLocationsOptionLocator = "li[title='{0}']";
        public CareersSearchPage() { }

        public IWebElement LanguageTextBox => BrowserFactory.Driver.FindElement(languageTextBoxLocator);
        public IWebElement LocationDropdownButton => BrowserFactory.Driver.FindElement(locationDropdownButtonLocator);
        public IWebElement LocationDropdownMenu => BrowserFactory.Driver.FindElement(locationDropDownMenuLocator);
        public IWebElement RemoteCheckbox => BrowserFactory.Driver.FindElement(remoteCheckboxLocator);
        public IWebElement FindButton => BrowserFactory.Driver.FindElement(findButtonLocator);
        public IWebElement SelectedLocation => BrowserFactory.Driver.FindElement(selectedLocationLocator);
        public IWebElement ViewAndApplyButton => BrowserFactory.Driver.FindElement(viewAndApplyButtonLocator);
        public IWebElement ViewMoreButton => BrowserFactory.Driver.FindElement(viewMoreButtonLocator);
        public IWebElement GetAllLocationsOption(string option)
        {
            allLocationsOptionLocator = string.Format(allLocationsOptionLocator, option);
            return LocationDropdownMenu.FindElement(By.CssSelector(allLocationsOptionLocator));
        }
    }
}
