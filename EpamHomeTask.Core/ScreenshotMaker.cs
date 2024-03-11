using OpenQA.Selenium.Support.Extensions;
using System.Drawing;
using System.Drawing.Imaging;

namespace EpamHomeTask.Core
{
    public class ScreenshotMaker
    {
        private static string NewScreenshotName
        {
            get { return "_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff") + "." + ScreenshotImageFormat; }
        }
        private static ImageFormat ScreenshotImageFormat
        {
            get { return ImageFormat.Jpeg; }
        }
        public static string TakeBrowserScreenshot()
        {
            var screenshotPath = Path.Combine(Environment.CurrentDirectory, "Display" + NewScreenshotName);
            var image = BrowserFactory.Driver.TakeScreenshot();
            image.SaveAsFile(screenshotPath);
            return screenshotPath;
        }
    }
}
