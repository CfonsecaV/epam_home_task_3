using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace epam_home_task
{
    public class Tests
    {
        private IWebDriver driver;

        //Locators for ValidateCriteriaBasedPositionSearch
        private readonly string acceptCookiesButtonLocator = "//button[contains(text(), 'Accept')]"; //Xpath
        private readonly string languageTextBoxLocator = "new_form_job_search-keyword"; //Id
        private string allLocationsOptionLocator = "li[title='{0}']"; //css
        private readonly string locationDropdownButtonLocator = "span[role=presentation]"; //css
        private readonly string dropDownMenuLocator = "ul[role] .os-content"; //css
        private readonly string careersTopMenuLocator = "//a[@class='top-navigation__item-link js-op'][.='Careers']"; //Xpath
        private readonly string remoteCheckboxLocator = "[name=remote]+label"; //css
        private readonly string findButtonLocator = "//button[contains(text(), 'Find')]"; //Xpath
        private readonly string viewAndApplyButtonLocator = "li.search-result__item:last-child .button-text"; //css
        private readonly string viewMoreButtonLocator = "//a[.='View More']"; //Xpath

        //Locators for ValidateGlobalSearch
        private readonly string searchIconLocator = "span.search-icon"; //css
        private readonly string searchTextBoxLocator = "q"; //Name
        private readonly string mainFindButtonLocator = "//span[contains(text(), 'Find')]"; //Xpath
        private readonly string listElementLocator = ".search-results__title-link"; //css
        private readonly string footerLocator = "footer.search-results__footer"; //css

        private string pageSource;

        public void AcceptCookies(WebDriverWait wait)
        {
            var condition1 = wait.Until(d => d.FindElement(By.XPath(acceptCookiesButtonLocator)));
            var condition2 = wait.Until(d => d.FindElement(By.XPath(acceptCookiesButtonLocator)).Enabled);
            IWebElement acceptCookiesButton = driver.FindElement(By.XPath(acceptCookiesButtonLocator));

            acceptCookiesButton.Click();
        }

        public bool WaitDisplayed(WebDriverWait wait ,By locator)
        {
            try
            {
                return wait.Until(d => d.FindElement(locator).Displayed);
            }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
        }
        
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
            Actions actions = new Actions(driver);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            AcceptCookies(wait);
            
            IWebElement careersTopMenu = driver.FindElement(By.XPath(careersTopMenuLocator));
            careersTopMenu.Click();
                        
            IWebElement languageTextBox = driver.FindElement(By.Id(languageTextBoxLocator));
            languageTextBox.SendKeys(programmingLanguage);

            IWebElement locationDropdownButton = driver.FindElement(By.CssSelector(locationDropdownButtonLocator));
            actions.MoveToElement(locationDropdownButton);
            locationDropdownButton.Click();

            IWebElement dropDownMenu = driver.FindElement(By.CssSelector(dropDownMenuLocator));
            actions.MoveToElement(dropDownMenu).Perform();

            allLocationsOptionLocator = string.Format(allLocationsOptionLocator, location);
            IWebElement allLocationsOption = dropDownMenu.FindElement(By.CssSelector(allLocationsOptionLocator));
            allLocationsOption.Click();

            //var condition1 = wait.Until(d => d.FindElement(By.CssSelector(remoteCheckboxLocator)).Displayed);
            WaitDisplayed(wait, By.CssSelector(remoteCheckboxLocator));
            IWebElement remoteCheckbox = driver.FindElement(By.CssSelector(remoteCheckboxLocator));
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)driver;
            javaScriptExecutor.ExecuteScript("arguments[0].click();", remoteCheckbox);

            IWebElement findButton = driver.FindElement(By.XPath(findButtonLocator));
            findButton.Click();

            IWebElement viewMoreButton = driver.FindElement(By.XPath(viewMoreButtonLocator));
            actions.ScrollToElement(viewMoreButton);

            WaitDisplayed(wait, By.CssSelector(viewAndApplyButtonLocator));
            IWebElement viewAndApplyButton = driver.FindElement(By.CssSelector(viewAndApplyButtonLocator));
            viewAndApplyButton.Click();

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
            Actions actions = new Actions(driver);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            AcceptCookies(wait);

            IWebElement searchIcon = driver.FindElement(By.CssSelector(searchIconLocator));
            searchIcon.Click();

            WaitDisplayed(wait, By.Name(searchTextBoxLocator));
            IWebElement searchTextBox = driver.FindElement(By.Name(searchTextBoxLocator));
            searchTextBox.SendKeys(keyword);

            IWebElement mainFindButton = driver.FindElement(By.XPath(mainFindButtonLocator));
            mainFindButton.Click();

            IWebElement footer = driver.FindElement(By.CssSelector(footerLocator));
            actions.ScrollToElement(footer).Perform();

            IList<IWebElement> listElements = driver.FindElements(By.CssSelector(listElementLocator));

            var filteredElements = listElements
                        .Where(element => element.GetAttribute("href").Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        .ToList();             
            Console.WriteLine(listElements);
            Assert.That(filteredElements, Is.EqualTo(listElements));


            driver.Quit();
        }
    }
}