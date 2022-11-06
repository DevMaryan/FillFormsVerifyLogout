using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSix
{
    [TestFixture]
    public class Setup
    {
        public IWebDriver _webDriver;
        public WebDriverWait _wait;
        public string webUrl = "http://18.156.17.83:9095/";
        public Random random = new Random();

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();

            _webDriver.Manage().Window.Maximize();
            _webDriver.Navigate().GoToUrl(webUrl);
            _webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            //_webDriver.Quit();
            //_webDriver.Dispose();
        }
    }
}
