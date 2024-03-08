using EpamHomeTask.Business.AplicationInterface;
using EpamHomeTask.Core;

namespace EpamHomeTask.Business.Business
{
    public class AboutPageContext
    {
        private AboutPage page = new();

        public void ScrollToEpamAtSection() => BrowserHelper.GetAction().ScrollToElement(page.EpamAtSection);
        public void ClickDownloadButton()
        {
            BrowserHelper.GetAction().MoveToElement(page.DownloadButton);
            page.DownloadButton.Click();
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
