using CommunityPlusAutomationFramework.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlusAutomationFramework
{
    public class BaseTest
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        protected static IWebDriver driver { get; set; }

        public static TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        public static string currentBrowser { get; set; }

        public IWebDriver GetWebDriver(string browserType)
        {
            SetUpBrowser(browserType);

            return driver;
        }

        public BrowserType GetBrowserType(string browserType)
        {
            switch (browserType)
            {
                case "Chrome":
                    return BrowserType.Chrome;
                case "FireFox":
                    return BrowserType.FireFox;
                default:
                    return BrowserType.Chrome;
            }
        }

        private void SetUpBrowser(string browserType)
        {
            var type = GetBrowserType(browserType);
            switch(type)
            {
                case BrowserType.Chrome:

                    var options = new ChromeOptions();
                    driver = new ChromeDriver(options);
                    break;

                case BrowserType.FireFox:

                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    driver = new FirefoxDriver(firefoxOptions);
                    break;
            }
        }

        [TestInitialize]
        public void InitializeForEachTest()
        {
            currentBrowser = testContextInstance.DataRow["Browser"] as string;

            driver = GetWebDriver(currentBrowser);

            driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void CleanUpForEachTest()
        {
            if (driver == null)
                return;
            driver.Quit();
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
        }
    }
}
