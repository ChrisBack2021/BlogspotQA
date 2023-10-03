using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blogspot
{
    [TestClass]
    public class BaseTest
    {
        public IWebDriver driver;
        string url = "https://testautomationpractice.blogspot.com/";

        [TestInitialize]
        public void InitialTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }
    }
}
