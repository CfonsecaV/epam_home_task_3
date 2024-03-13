using EpamHomeTask.Business.Pages;
using EpamHomeTask.Core;
using OpenQA.Selenium;

namespace EpamHomeTask.Business.Business
{
    public class AboutPageContext
    {
        private AboutPage _page;

        public AboutPageContext(IWebDriver webDriver) 
        {
            _page = new AboutPage(webDriver);
        }

        public void ScrollToEpamAtSection() => BrowserHelper.GetAction(_page.Driver).ScrollToElement(_page.EpamAtSection);
        public void ClickDownloadButton()
        {
            BrowserHelper.GetAction(_page.Driver).MoveToElement(_page.DownloadButton);
            _page.DownloadButton.Click();
        }
        public bool CheckDownload(string downloadedFile)
        {
            Thread.Sleep(4000);
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            List<string> files = Directory.GetFiles(downloadPath).ToList();
            return LoopFiles(files, downloadedFile);
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
