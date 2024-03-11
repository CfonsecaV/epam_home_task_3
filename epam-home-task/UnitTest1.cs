using NUnit.Framework;
using EpamHomeTask.Business.Business;
using EpamHomeTask.Core;

namespace EpamHomeTask.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            BrowserFactory.InitBrowser("Chrome");
            BrowserFactory.LoadApplication();
        }

        [TearDown]
        public void TearDown()
        {
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

            Assert.That(careerSearchPage.GetSelectedLocationText(), Does.Contain(location));
            
            careerSearchPage.ClickRemoteCheckbox();
            careerSearchPage.ClickFindButton();
            careerSearchPage.ScrollToViewMoreButton();            
            careerSearchPage.ClickViewAndApplyButton();

            Assert.That(BrowserHelper.GetPageSource(), Does.Contain(programmingLanguage));
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

            Assert.That(searchResultPage.FilterList(keyword), Is.EqualTo(searchResultPage.GetListElements())
                , $"Filtered list amount({searchResultPage.FilterList(keyword).Count}) " +
                $"is not the same as in Total list ({searchResultPage.GetListElements().Count})");

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

            Assert.That(aboutPage.CheckDownload(file), "File isn't downloaded");
        }

        [Test]
        public void ArticleTitleIsTheSameAsNotedTitle_ItIsTheSame_Pass() { 
            HomePageContext home = HomePageContext.Open();
            string? activeTitle;

            home.AcceptCookies();
            InsightPageContext insight = home.ClickInsightButton();
            insight.SlideSetAmountOfElements(2);            
            activeTitle = insight.SaveActiveElementTitle();

            Assert.That(activeTitle, Does.Contain("From Taming Cloud Complexity"), "Wrong slide element");

            insight.ClickActiveReadMoreButton();
            insight.ScrollToArticleTitle();

            Assert.That(activeTitle, Is.EqualTo(insight.GetArticleTitle()),
                $"The title ({activeTitle}) is not the same as ({insight.GetArticleTitle()})");

        }
    }
}