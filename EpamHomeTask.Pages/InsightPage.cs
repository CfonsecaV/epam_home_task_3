using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamHomeTask.Pages
{
    public class InsightPage
    {
        private IWebDriver driver;        

        public InsightPage(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
