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
        public static void WaitCondition(Func<bool> condition)
        {
            WebDriverWait wait = new(BrowserFactory.Driver, TimeSpan.FromSeconds(10));
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
        public static Actions GetAction()
        {
            Actions action = new(BrowserFactory.Driver);
            return action;
        }

        public static string GetPageSource()
        {
            string pageSource = BrowserFactory.Driver.PageSource;
            return pageSource;
        }
    }
}
