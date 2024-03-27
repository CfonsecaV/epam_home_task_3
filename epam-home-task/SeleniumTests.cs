using NUnit.Framework;
using EpamHomeTask.Business.Context;
using EpamHomeTask.Core;
using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace EpamHomeTask.Tests
{
    public class SeleniumTests
    {   
        private IWebDriver _driver;
        private HomePageContext _homeContext;
        private string _downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");
        protected ILog Log
        {
            get { return LogManager.GetLogger(this.GetType()); }
        }

        [SetUp]
        public void Setup()
        {
            var browser = TestContext.Parameters.Get("BROWSER");
            XmlConfigurator.Configure(new FileInfo("Log.config"));
            Log.Info($"Initializing '{browser}' Browser...");
            BrowserFactory factory = new(Enum.Parse<Browsers>(browser, true), _downloadPath);
            _driver = factory.GetInstanceOf();
            _homeContext = new(_driver);
            _homeContext.Open();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotMaker.TakeBrowserScreenshot(_driver);
            }
            Log.Info("Closing browser...");
            BrowserFactory.CloseAllDrivers(_driver);
        }

        [Test]
        [TestCase("C#", "All Locations")]
        [TestCase("Java", "All Locations")]
        [TestCase("Python", "All Locations")]
        public void PageContainsInputProgrammingLanguage_DoesContainIt_Pass(string programmingLanguage, string location)
        {
            _homeContext.AcceptCookies();
            CareerSearchPageContext careerSearchPage = _homeContext.ClickCareerButton();
            careerSearchPage.InputProgrammingLanguage(programmingLanguage); 
            careerSearchPage.ClickLocationDropdownButton();
            careerSearchPage.ClickAllLocationsOption(location);

            Log.Info($"Checking if selected location is '{location}'...");
            Assert.That(careerSearchPage.GetSelectedLocationText(), Does.Contain(location));

            Log.Info("Location is the expected one");

            careerSearchPage.ClickRemoteCheckbox();
            careerSearchPage.ClickFindButton();
            careerSearchPage.ScrollToViewMoreButton();            
            careerSearchPage.ClickViewAndApplyButton();

            Log.Info($"Checking if page source contains '{programmingLanguage}'...");
            Assert.That(BrowserHelper.GetPageSource(_driver), Does.Contain(programmingLanguage));

            Log.Info($"Page source contains '{programmingLanguage}'");
        }

        [Test]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void KeywordIsPresentInAllItemsOfList_IsNotPresentInAll_Fail(string keyword)
        {
            _homeContext.AcceptCookies();
            _homeContext.ClickSearchIcon();
            _homeContext.InputSearchKeyword(keyword);
            SearchResultPageContext searchResultPage = _homeContext.ClickMainFindButton();
            searchResultPage.ScrollToFooter();

            Log.Info($"Checking if all elements on the list contain '{keyword}'...");
            Assert.That(searchResultPage.FilterList(keyword), Is.EqualTo(searchResultPage.GetListElements())
                , $"Filtered list amount({searchResultPage.FilterList(keyword).Count}) " +
                $"is not the same as in Total list ({searchResultPage.GetListElements().Count})");

            Log.Info($"All elements contain '{keyword}'");
        }

        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
        public void DownloadButtonDownloadsFile_FileDownloadsCorrectly_Pass(string file)
        {
            _homeContext.AcceptCookies();
            AboutPageContext aboutPage = _homeContext.ClickAboutButton();
            aboutPage.ScrollToEpamAtSection();
            aboutPage.ClickDownloadButton();
            
            Log.Info("Downloading file...");
            if(!Directory.Exists(_downloadPath))
            {
                Directory.CreateDirectory(_downloadPath);
            }

            Assert.That(aboutPage.CheckDownload(file, _downloadPath), "File isn't downloaded");
            ScreenshotMaker.TakeBrowserScreenshot(_driver);

            Log.Info($"Correctly downloaded file '{file}'");
        }

        [Test]
        public void ArticleTitleIsTheSameAsNotedTitle_ItIsTheSame_Pass() 
        { 
            string? activeTitle;

            _homeContext.AcceptCookies();
            InsightPageContext insight = _homeContext.ClickInsightButton();
            insight.SlideSetAmountOfElements(2);            
            activeTitle = insight.SaveActiveElementTitle();

            Log.Info("Checking if slide element is the desired one...");
            Assert.That(activeTitle, Does.Contain("From Taming Cloud Complexity"), "Wrong slide element");

            insight.ClickActiveReadMoreButton();
            insight.ScrollToArticleTitle();

            Log.Info("Checking article title...");
            Assert.That(activeTitle, Is.EqualTo(insight.GetArticleTitle()),
                $"The title ({activeTitle}) is not the same as ({insight.GetArticleTitle()})");

            Log.Info("Article title is the expected one");
        }
    }
}