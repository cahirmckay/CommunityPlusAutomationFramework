using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CommunityPlusAutomationFramework
{
    public static class IWebDriverExtension
    {
        public static IWebElement WaitUntilElementIsClickable(this IWebDriver Driver, By by, int timeout = 20)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            IWebElement element = wait.Until(ElementToBeClickable(by));
            return element;
        }

        public static IWebElement WaitUntilElementIsVisible(this IWebDriver Driver, By by, int timeout = 20)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            IWebElement element = wait.Until(ElementIsVisible(by));
            return element;
        }
    }
}
