using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using EpamHomeTask.Pages;
using OpenQA.Selenium.Internal;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace EpamHomeTask.Tests
{
    public class Tests
    {
        private IWebDriver driver;
        private string? pageSource;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var configuration = configBuilder.Build();
            string baseUrl = configuration["BaseUrl"] ?? string.Empty;
            driver.Navigate().GoToUrl(baseUrl);

        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        [TestCase("C#", "All Locations")]
        [TestCase("Java", "All Locations")]
        [TestCase("Python", "All Locations")]
        public void PageContainsInputProgrammingLanguage_DoesContainIt_Pass(string programmingLanguage, string location)
        {
            HomePage homePage = new(driver);

            homePage.AcceptCookies();
            CareersSearchPage careersSearchPage = homePage.ClickCareerButton();
            careersSearchPage.InputProgrammingLanguage(programmingLanguage);
            careersSearchPage.ClickLocationDropdownButton();
            careersSearchPage.ClickAllLocationsOption(location);

            Assert.That(careersSearchPage.GetSelectedLocation().Text, Does.Contain(location));

            GeneralMethods.WaitCondition(driver, () => careersSearchPage.GetRemoteCheckbox().Displayed);
            careersSearchPage.ClickRemoteCheckbox();
            careersSearchPage.ClickFindButton();
            careersSearchPage.ScrollToViewMoreButton(driver);
            GeneralMethods.WaitCondition(driver, () => careersSearchPage.GetViewAndApplyButton().Displayed);
            careersSearchPage.ClickViewAndApplyButton();
            pageSource = driver.PageSource;

            Assert.That(pageSource, Does.Contain(programmingLanguage));
        }

        [Test]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void KeywordIsPresentInAllItemsOfList_IsNotPresentInAll_Fail(string keyword)
        {
            HomePage homePage = new(driver);

            homePage.AcceptCookies();
            homePage.ClickSearchIcon();
            GeneralMethods.WaitCondition(driver, () => homePage.GetSearchTextBox().Displayed);
            homePage.InputSearchKeyword(keyword);
            SearchResultPage searchResultPage = homePage.ClickMainFindButton();
            searchResultPage.ScrollToFooter(driver);             

            Assert.That(searchResultPage.FilterList(keyword), Is.EqualTo(searchResultPage.GetListElements())
                , $"Filtered list amount({searchResultPage.FilterList(keyword).Count}) " +
                $"is not the same as in Total list ({searchResultPage.GetListElements().Count})");

        }

        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
        public void DownloadButtonDownloadsFile_FileDownloadsCorrectly_Pass(string file)
        {
            HomePage homePage = new(driver);

            homePage.AcceptCookies();
            AboutPage aboutPage =  homePage.ClickAboutButton();
            aboutPage.ScrollToEpamAtSection(driver);
            aboutPage.ClickDownloadButton(driver);

            Assert.That(aboutPage.CheckDownload(file), "File isn't downloaded");
        }

        [Test]
        public void ArticleTitleIsTheSameAsNotedTitle_ItIsTheSame_Pass() { 
            HomePage homePage = new(driver);
            string? activeTitle;

            homePage.AcceptCookies();
            InsightPage insightPage = homePage.ClickInsightButton();
            insightPage.SlideSetAmountOfElements(2);            
            activeTitle = insightPage.SaveActiveElementTitle();

            Assert.That(activeTitle, Does.Contain("From Taming Cloud Complexity"), "Wrong slide element");

            insightPage.ClickActiveReadMoreButton(driver);
            insightPage.ScrollToArticleTitle(driver);

            Assert.That(activeTitle, Is.EqualTo(insightPage.GetArticleTitle().Text),
                $"The title ({activeTitle}) is not the same as ({insightPage.GetArticleTitle().Text})");

        }
    }
}