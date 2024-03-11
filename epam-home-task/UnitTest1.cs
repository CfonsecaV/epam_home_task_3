using NUnit.Framework;
using EpamHomeTask.Business.Business;
using EpamHomeTask.Business.Data;
using EpamHomeTask.Core;
using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;

namespace EpamHomeTask.Tests
{
    public class Tests
    {     
        protected ILog Log
        {
            get { return LogManager.GetLogger(this.GetType()); }
        }

        [SetUp]
        public void Setup()
        {
            XmlConfigurator.Configure(new FileInfo("Log.config"));
            Log.Info($"Initializing '{Data.BrowserTypes[0]}' Browser...");
            BrowserFactory.InitBrowser(Data.BrowserTypes[0].ToString());
            BrowserFactory.LoadApplication();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotMaker.TakeBrowserScreenshot();
            }
            Log.Info("Closing browser...");
            BrowserFactory.CloseAllDrivers();
        }

        [Test]
        [TestCase("C#", "All Locations")]
        [TestCase("Java", "All Locations")]
        [TestCase("Python", "All Locations")]
        public void PageContainsInputProgrammingLanguage_DoesContainIt_Pass(string programmingLanguage, string location)
        {
            HomePageContext homePage = HomePageContext.Open();

            homePage.AcceptCookies();
            CareerSearchPageContext careerSearchPage = homePage.ClickCareerButton();
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
            Assert.That(BrowserHelper.GetPageSource(), Does.Contain(programmingLanguage));

            Log.Info($"Page source contains '{programmingLanguage}'");
        }

        [Test]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void KeywordIsPresentInAllItemsOfList_IsNotPresentInAll_Fail(string keyword)
        {
            HomePageContext homePage = HomePageContext.Open();

            homePage.AcceptCookies();
            homePage.ClickSearchIcon();
            homePage.InputSearchKeyword(keyword);
            SearchResultPageContext searchResultPage = homePage.ClickMainFindButton();
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
            HomePageContext homePage = HomePageContext.Open();

            homePage.AcceptCookies();
            AboutPageContext aboutPage = homePage.ClickAboutButton();
            aboutPage.ScrollToEpamAtSection();
            aboutPage.ClickDownloadButton();
            
            Log.Info("Downloading file...");
            Assert.That(aboutPage.CheckDownload(file), "File isn't downloaded");
            ScreenshotMaker.TakeBrowserScreenshot();

            Log.Info($"Correctly downloaded file '{file}'");
        }

        [Test]
        public void ArticleTitleIsTheSameAsNotedTitle_ItIsTheSame_Pass() { 
            HomePageContext home = HomePageContext.Open();
            string? activeTitle;

            home.AcceptCookies();
            InsightPageContext insight = home.ClickInsightButton();
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