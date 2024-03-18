using OpenQA.Selenium;
using EpamHomeTask.Business.Context;
using EpamHomeTask.Core;
using NUnit.Framework;


namespace SpecFlowProject.Specs.StepDefinitions
{
    [Binding]
    public class ArticleTitleCheckStepDefinitions
    {
        private IWebDriver? _driver;
        private InsightPageContext? _insight;
        private HomePageContext? _homeContext;
        string? _activeTitle;

        [BeforeScenario("EPAM")] public void BeforeScenario()
        {
            BrowserFactory factory = new(Browsers.Edge);
            _driver = factory.GetInstanceOf();
            _homeContext = new(_driver);
        }

        [Given(@"I'm on the Epam main page")]
        public void GivenImOnTheEpamMainPage()
        {
            _homeContext?.Open();
            _homeContext?.AcceptCookies();
        }

        [When(@"I click on the Insight button")]
        public void WhenIClickOnTheInsightButton()
        {
            _insight = _homeContext?.ClickInsightButton();            
        }

        [When(@"I go to the third Slide")]
        public void WhenIGoToTheThirdSlide()
        {
            _insight?.SlideSetAmountOfElements(2);
            _activeTitle = _insight?.SaveActiveElementTitle();
            Assert.That(_activeTitle, Does.Contain("From Taming Cloud Complexity"), "Wrong slide element");
        }

        [When(@"I click on the read more button")]
        public void WhenIClickOnTheReadMoreButton()
        {
            _insight?.ClickActiveReadMoreButton();
        }

        [Then(@"The article name is the same as the one in the slide")]
        public void ThenTheArticleNameIsTheSameAsTheOneInTheSlide()
        {
            _insight?.ScrollToArticleTitle();
            Assert.That(_activeTitle, Is.EqualTo(_insight?.GetArticleTitle()),
                $"The title ({_activeTitle}) is not the same as ({_insight?.GetArticleTitle()})");
        }

        [AfterScenario("EPAM")] public void AfterScenario()
        {
            BrowserFactory.CloseAllDrivers(_driver);
        }
    }
}
