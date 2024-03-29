using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
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
        public static string TakeBrowserScreenshot(IWebDriver webDriver)
        {
            var screenshotPath = Path.Combine(Environment.CurrentDirectory, "Screenshots", "Display" + NewScreenshotName);
            var image = webDriver.TakeScreenshot();
            image.SaveAsFile(screenshotPath);
            return screenshotPath;
        }
    }
}
