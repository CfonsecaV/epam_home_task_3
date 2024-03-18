using OpenQA.Selenium;
using EpamHomeTask.Business.Context;
using EpamHomeTask.Core;
using NUnit.Framework;

namespace SpecFlowProject.Specs.StepDefinitions
{
    [Binding]
    public class SpecflowDocsSearchStepDefinitions
    {
        private IWebDriver? _driver;
        private SpecflowHomePageContext? _homeContext;
        private SpecflowDocsPageContext? _docs;

        [BeforeScenario("Specflow")] public void BeforeScenario()
        {
            BrowserFactory factory = new(Browsers.Chrome);
            _driver = factory.GetInstanceOf();
            _homeContext = new(_driver);
        }

        [Given(@"I open the official Specflow site")]
        public void GivenIOpenTheOfficialSpecflowSite()
        {
            _homeContext?.Open();
            _homeContext?.AcceptCookies();
        }

        [When(@"I hover the Docs button at the top menu")]
        public void WhenIHoverTheDocsButtonAtTheTopMenu()
        {
            _homeContext?.MoveToDocsTopMenu();
        }

        [When(@"I click con the Specflow documentation option")]
        public void WhenIClickConTheSpecflowDocumentationOption()
        {
            _docs = _homeContext?.SelectSpecflowDocumentation();
        }

        [When(@"I click on the search bar")]
        public void WhenIClickOnTheSearchBar()
        {
            _docs?.SelectSearchBar();
        }

        [When(@"I input ""([^""]*)"" into the pop up search bar")]
        public void WhenIInputIntoThePopUpSearchBar(string installation)
        {
            _docs?.InputKeyword(installation);
        }

        [When(@"I select the first result")]
        public void WhenISelectTheFirstResult()
        {
            _docs?.ClickSearchResult();
        }

        [Then(@"Then the result page title contains ""([^""]*)""")]
        public void ThenThenTheResultPageTitleContains(string installation)
        {
            Assert.That(_docs?.GetResultTitle(), Is.EqualTo(installation));
        }

        [AfterScenario("Specflow")]
        public void AfterScenario()
        {
            BrowserFactory.CloseAllDrivers(_driver);
        }
    }
}
