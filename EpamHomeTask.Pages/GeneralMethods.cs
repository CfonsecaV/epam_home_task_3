using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Pages
{
    public static class GeneralMethods
    {
        public static void WaitCondition(IWebDriver driver, Func<bool> condition)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
    }
}
