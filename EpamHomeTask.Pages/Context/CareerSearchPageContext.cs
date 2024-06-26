﻿using EpamHomeTask.Business.Pages;
using OpenQA.Selenium;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.Context
{
    public class CareerSearchPageContext
    {
        private CareersSearchPage _page;

        public CareerSearchPageContext(IWebDriver webDriver)
        {
            _page = new CareersSearchPage(webDriver);
        }

        public void InputProgrammingLanguage(string language) => _page.LanguageTextBox.SendKeys(language);
        public void MoveToLocationDropdownButton() => BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.LocationDropdownButton);
        public void MoveToLocationDropdownMenu() => BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.LocationDropdownMenu);
        public void MoveToViewAndApplyButton() => BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.ViewAndApplyButton);
        public string GetSelectedLocationText() => _page.SelectedLocation.Text;
        public void ClickLocationDropdownButton()
        {
            MoveToLocationDropdownButton();
            _page.LocationDropdownButton.Click();
        }
        public void ClickViewAndApplyButton()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.ViewAndApplyButton.Displayed);
            MoveToViewAndApplyButton();
            _page.ViewAndApplyButton.Click();
        }
        public void ClickRemoteCheckbox()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.RemoteCheckbox.Displayed);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_page.Driver;
            jse.ExecuteScript("arguments[0].click();", _page.RemoteCheckbox);
        }
        public void ClickFindButton()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_page.Driver;
            jse.ExecuteScript("arguments[0].click();", _page.FindButton);
        }
        public void ClickAllLocationsOption(string option)
        {
            MoveToLocationDropdownMenu();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_page.Driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", _page.GetAllLocationsOption(option));
            _page.GetAllLocationsOption(option).Click();
        }
        public void ScrollToViewMoreButton()
        {
            BrowserHelper.WaitCondition(_page.Driver, () => _page.ViewMoreButton.Displayed);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_page.Driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", _page.ViewMoreButton);
            //BrowserHelper.GetAction(_page.Driver).ScrollToElement(_page.ViewMoreButton);
        }
    }
}
