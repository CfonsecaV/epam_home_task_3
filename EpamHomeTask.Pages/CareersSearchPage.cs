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
    public class CareersSearchPage
    {
        private IWebDriver driver;

        private readonly By languageTextBoxLocator = By.Id("new_form_job_search-keyword");
        private readonly By locationDropdownButtonLocator = By.CssSelector("span[role=presentation]");
        private readonly By locationDropDownMenuLocator = By.CssSelector("ul[role] .os-content") ;
        private readonly By remoteCheckboxLocator = By.CssSelector("[name=remote]+label");
        private readonly By selectedLocationLocator = By.CssSelector(".select2-selection__rendered"); 
        private readonly By findButtonLocator = By.XPath("//button[contains(text(), 'Find')]");
        private readonly By viewMoreButtonLocator = By.XPath("//a[.='View More']");
        private readonly By viewAndApplyButtonLocator = By.CssSelector("li.search-result__item:last-child .button-text");
        private string allLocationsOptionLocator = "li[title='{0}']";
        public CareersSearchPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement GetLanguageTextBox() { return driver.FindElement(languageTextBoxLocator); }
        public IWebElement GetLocationDropdownButton() { return driver.FindElement(locationDropdownButtonLocator); }
        public IWebElement GetLocationDropdownMenu() { return driver.FindElement(locationDropDownMenuLocator); }
        public IWebElement GetRemoteCheckbox() { return driver.FindElement(remoteCheckboxLocator); }
        public IWebElement GetFindButton() { return driver.FindElement(findButtonLocator); }
        public IWebElement GetSelectedLocation() { return driver.FindElement(selectedLocationLocator); }
        public IWebElement GetViewAndApplyButton() { return driver.FindElement(viewAndApplyButtonLocator); }
        public IWebElement GetViewMoreButton() { return driver.FindElement(viewMoreButtonLocator); }
        public IWebElement GetAllLocationsOption(string option) {
            allLocationsOptionLocator = string.Format(allLocationsOptionLocator, option);
            return GetLocationDropdownMenu().FindElement(By.CssSelector(allLocationsOptionLocator));
        }

        public void InputProgrammingLanguage(string language)
        {
            GetLanguageTextBox().SendKeys(language);
        }

        public void ClickAllLocationsOption(string option)
        {
            MoveToLocationDropdownMenu(driver);
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", GetAllLocationsOption(option));
            GetAllLocationsOption(option).Click();
        }

        public void ClickRemoteCheckbox()
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", GetRemoteCheckbox());
        }

        public void ClickFindButton()
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", GetFindButton());
        }

        public void ClickViewAndApplyButton()
        {
            MoveToViewAndApplyButton(driver);
            GetViewAndApplyButton().Click();
        }

        public void ClickLocationDropdownButton()
        {
            MoveToLocationDropdownButton(driver);
            GetLocationDropdownButton().Click();
        }

        public void MoveToLocationDropdownButton(IWebDriver driver)
        {
            Actions action = new(driver);
            action.MoveToElement(GetLocationDropdownButton());
        }

        public void MoveToLocationDropdownMenu(IWebDriver driver)
        {
            Actions action = new(driver);
            action.MoveToElement(GetLocationDropdownMenu());
        }

        public void ScrollToViewMoreButton(IWebDriver driver)
        {   
            Actions action = new(driver);
            action.ScrollToElement(GetViewMoreButton());
        }

        public void MoveToViewAndApplyButton(IWebDriver driver)
        {
            Actions action = new(driver);
            action.MoveToElement(GetViewAndApplyButton());
        }

    }
}
