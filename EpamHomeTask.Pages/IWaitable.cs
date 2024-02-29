using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Pages
{
    public interface IWaitable
    {
        bool WaitDisplayed(WebDriverWait wait, IWebElement element);
    }
}
