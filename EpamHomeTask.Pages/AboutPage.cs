using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Pages
{
    public class AboutPage
    {
        private IWebDriver driver;
        private readonly By epamAtSectionLocator = By.XPath("//span[contains(text(), 'EPAM at')]");
        private readonly By downloadButtonLocator = By.CssSelector("a[href*='EPAM_Corporate']");

        public AboutPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement GetEpamAtSection() { return driver.FindElement(epamAtSectionLocator); }
        public IWebElement GetDownloadButton() {  return driver.FindElement(downloadButtonLocator); }  


        public void ScrollToEpamAtSection(IWebDriver driver)
        {
            Actions action = new(driver);
            action.ScrollToElement(GetEpamAtSection());
        }

        public void ClickDownloadButton(IWebDriver driver)
        {
            Actions action = new(driver);
            action.MoveToElement(GetDownloadButton());
            GetDownloadButton().Click();
        }

        public bool CheckDownload(string downloadedFile)
        {
            Thread.Sleep(4000);
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            List<string> files = Directory.GetFiles(downloadPath).ToList();             
            return LoopFiles(files,downloadedFile);
        }

        private bool LoopFiles(List<string> files, string downloadedFile)
        {
            bool isDownloaded = false;
            foreach (string file in files)
            {
                if (Path.GetFileName(file).Contains(downloadedFile))
                {
                    isDownloaded = true;
                    File.Delete(file);
                    break;
                }
            }
            return isDownloaded;
        }
    }
}
