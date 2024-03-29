using EpamHomeTask.Core;
using OpenQA.Selenium;

namespace EpamHomeTask.Business.Pages
{
    public class CareersSearchPage
    {
        private readonly IWebDriver _webDriver;

        private readonly By languageTextBoxLocator = By.Id("new_form_job_search-keyword");
        private readonly By locationDropdownButtonLocator = By.CssSelector("span[role=presentation]");
        private readonly By locationDropDownMenuLocator = By.CssSelector("ul[role] .os-content");
        private readonly By remoteCheckboxLocator = By.CssSelector("[name=remote]+label");
        private readonly By selectedLocationLocator = By.CssSelector(".select2-selection__rendered");
        private readonly By findButtonLocator = By.XPath("//button[contains(text(), 'Find')]");
        private readonly By viewMoreButtonLocator = By.CssSelector("a[href='#' i]");
        private readonly By viewAndApplyButtonLocator = By.CssSelector("li.search-result__item:last-child .button-text");
        private string allLocationsOptionLocator = "li[title='{0}']";
        public CareersSearchPage(IWebDriver webDriver) 
        {
            _webDriver = webDriver;
        }
        public IWebDriver Driver => _webDriver;
        public IWebElement LanguageTextBox => _webDriver.FindElement(languageTextBoxLocator);
        public IWebElement LocationDropdownButton => _webDriver.FindElement(locationDropdownButtonLocator);
        public IWebElement LocationDropdownMenu => _webDriver.FindElement(locationDropDownMenuLocator);
        public IWebElement RemoteCheckbox => _webDriver.FindElement(remoteCheckboxLocator);
        public IWebElement FindButton => _webDriver.FindElement(findButtonLocator);
        public IWebElement SelectedLocation => _webDriver.FindElement(selectedLocationLocator);
        public IWebElement ViewAndApplyButton => _webDriver.FindElement(viewAndApplyButtonLocator);
        public IWebElement ViewMoreButton => _webDriver.FindElement(viewMoreButtonLocator);
        public IWebElement GetAllLocationsOption(string option)
        {
            allLocationsOptionLocator = string.Format(allLocationsOptionLocator, option);
            return LocationDropdownMenu.FindElement(By.CssSelector(allLocationsOptionLocator));
        }
    }
}
