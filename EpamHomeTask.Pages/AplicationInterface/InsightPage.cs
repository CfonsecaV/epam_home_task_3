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
    public class InsightPage
    {
        private readonly By activeElementNextButtonLocator = By.CssSelector(".media-content button.slider__right-arrow");
        private readonly By mainSliderLocator = By.CssSelector(".content-container>div:first-child");
        private readonly By activeElementTitleLocator = By.CssSelector(".active .font-size-60");
        private readonly By activeElementReadMoreLocator = By.CssSelector(".active .link-with-bottom-arrow");
        private readonly By articleTitleLocator = By.CssSelector(".article__container span.museo-sans-700");
        private readonly By activeElementLocator = By.XPath("//div[@class='slider-ui-23   media-content ']//div[@class='owl-item active']");

        public InsightPage() { }
        
        public IWebElement ActiveElementNextButton => BrowserFactory.Driver.FindElement(activeElementNextButtonLocator);
        public IWebElement NewActiveElement => BrowserFactory.Driver.FindElement(activeElementLocator);
        public IWebElement MainSlider => BrowserFactory.Driver.FindElement(mainSliderLocator);
        public IWebElement ActiveElementTitle => BrowserFactory.Driver.FindElement(activeElementTitleLocator);
        public IWebElement ActiveElementReadMore => BrowserFactory.Driver.FindElement(activeElementReadMoreLocator);
        public IWebElement ArticleTitle => BrowserFactory.Driver.FindElement(articleTitleLocator);
    }
}
