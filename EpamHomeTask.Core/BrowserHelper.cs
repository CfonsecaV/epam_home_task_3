using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace EpamHomeTask.Core
{
    public class BrowserHelper
    {       
        public static void WaitCondition(IWebDriver webDriver ,Func<bool> condition)
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(10));
            wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            try
            {
                wait.Until(d => condition());
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Element was not found in time");
            }

        }
        public static Actions GetAction(IWebDriver webDriver)
        {
            Actions action = new(webDriver);
            return action;
        }

        public static string GetPageSource(IWebDriver webDriver)
        {
            string pageSource = webDriver.PageSource;
            return pageSource;
        }
    }
}
