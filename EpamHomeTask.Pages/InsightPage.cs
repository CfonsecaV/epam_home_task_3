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
    public class InsightPage
    {
        private IWebDriver driver;

        private readonly By activeElementNextButtonLocator = By.CssSelector(".media-content button.slider__right-arrow");
        private readonly By mainSliderLocator = By.CssSelector(".content-container>div:first-child");
        private readonly By activeElementTitleLocator = By.CssSelector(".active .font-size-60");
        private readonly By activeElementReadMoreLocator = By.CssSelector(".active .link-with-bottom-arrow");
        private readonly By articleTitleLocator = By.CssSelector(".article__container span.museo-sans-700");
        private readonly By activeElementLocator = By.XPath("//div[@class='slider-ui-23   media-content ']//div[@class='owl-item active']");
        

        public InsightPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebElement GetActiveElementNextButton() { return driver.FindElement(activeElementNextButtonLocator); }
        public IWebElement GetNewActiveElement() { return driver.FindElement(activeElementLocator); }
        public IWebElement GetMainSlider() { return driver.FindElement(mainSliderLocator); }
        public IWebElement GetActiveElementTitle() { return driver.FindElement(activeElementTitleLocator); }
        public IWebElement GetActiveElementReadMore() { return driver.FindElement(activeElementReadMoreLocator); }

        public IWebElement GetArticleTitle() { return driver.FindElement(articleTitleLocator); }

        public void SlideSetAmountOfElements(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GeneralMethods.WaitCondition(driver, () => GetActiveElementNextButton().Enabled);
                NextElement(driver);
                GeneralMethods.WaitCondition(driver, () => GetNewActiveElement().GetAttribute("aria-hidden").Equals("false"));
            }

        }
        public void NextElement(IWebDriver driver)
        {            
            Actions action = new(driver);            
            action.MoveToElement(GetActiveElementNextButton());
            GetActiveElementNextButton().Click();
        }
        public void ClickActiveReadMoreButton(IWebDriver driver)
        {
            Actions action = new(driver);
            action.MoveToElement(GetActiveElementReadMore());
            GeneralMethods.WaitCondition(driver, () => GetActiveElementReadMore().Displayed);
            GetActiveElementReadMore().Click();
        }
        public string SaveActiveElementTitle()
        {
            return GetActiveElementTitle().Text;
        }

        public void ScrollToArticleTitle(IWebDriver driver)
        {
            Actions action = new(driver);
            action.ScrollToElement(GetArticleTitle());
        }
       
    }
}
