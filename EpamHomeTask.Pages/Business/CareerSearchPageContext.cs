using EpamHomeTask.Business.AplicationInterface;
using OpenQA.Selenium;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.Business
{
    public class CareerSearchPageContext
    {
        private CareersSearchPage page = new();

        public void InputProgrammingLanguage(string language) => page.LanguageTextBox.SendKeys(language);
        public void MoveToLocationDropdownButton() => BrowserHelper.GetAction().MoveToElement(page.LocationDropdownButton);
        public void MoveToLocationDropdownMenu() => BrowserHelper.GetAction().MoveToElement(page.LocationDropdownMenu);
        public void ScrollToViewMoreButton() => BrowserHelper.GetAction().ScrollToElement(page.ViewMoreButton);
        public void MoveToViewAndApplyButton() => BrowserHelper.GetAction().MoveToElement(page.ViewAndApplyButton);
        public string GetSelectedLocationText() => page.SelectedLocation.Text;
        public void ClickLocationDropdownButton()
        {
            MoveToLocationDropdownButton();
            page.LocationDropdownButton.Click();
        }
        public void ClickViewAndApplyButton()
        {
            BrowserHelper.WaitCondition(() => page.ViewAndApplyButton.Displayed);
            MoveToViewAndApplyButton();
            page.ViewAndApplyButton.Click();
        }
        public void ClickRemoteCheckbox()
        {
            BrowserHelper.WaitCondition(() => page.RemoteCheckbox.Displayed);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)BrowserFactory.Driver;
            jse.ExecuteScript("arguments[0].click();", page.RemoteCheckbox);
        }
        public void ClickFindButton()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)BrowserFactory.Driver;
            jse.ExecuteScript("arguments[0].click();", page.FindButton);
        }
        public void ClickAllLocationsOption(string option)
        {
            MoveToLocationDropdownMenu();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)BrowserFactory.Driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", page.GetAllLocationsOption(option));
            page.GetAllLocationsOption(option).Click();
        }
    }
}
