using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using EpamHomeTask.Pages;

namespace EpamHomeTask.Tests
{
    public class Tests
    {
        private IWebDriver driver;
        private string pageSource;
        
        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.epam.com/");

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
        public void ValidateCriteriaBasedPositionSearch(string programmingLanguage, string location)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            HomePage homePage = new(driver);

            homePage.AcceptCookies(wait);
            CareersSearchPage careersSearchPage = homePage.ClickCareerButton();
            careersSearchPage.InputProgrammingLanguage(programmingLanguage);
            careersSearchPage.MoveToLocationDropDownButton(driver);
            careersSearchPage.ClickLocationDropdownButton();
            careersSearchPage.MoveToLocationDropDownMenu(driver);
            careersSearchPage.ClickAllLocationsOption(location);
            careersSearchPage.WaitDisplayed(wait,careersSearchPage.GetRemoteCheckbox());
            careersSearchPage.ClickRemoteCheckbox();
            careersSearchPage.ClickFindButton();
            careersSearchPage.ScrollToViewMoreButton(driver);
            careersSearchPage.WaitDisplayed(wait, careersSearchPage.GetViewAndApplyButton());
            careersSearchPage.ClickViewAndApplyButton();
            pageSource = driver.PageSource;

            Assert.That(pageSource, Does.Contain(programmingLanguage));
            driver.Quit();
        }

        [Test]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void ValidateGlobalSearch(string keyword)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            HomePage homePage = new(driver);

            homePage.AcceptCookies(wait);
            homePage.ClickSearchIcon();
            homePage.WaitDisplayed(wait, homePage.GetSearchTextBox());
            homePage.InputSearchKeyword(keyword);
            SearchResultPage searchResultPage = homePage.ClickMainFindButton();
            searchResultPage.ScrollToFooter(driver); 

            Assert.That(searchResultPage.FilterList(keyword), Is.EqualTo(searchResultPage.GetListElements()));
            driver.Quit();
        }
    }
}