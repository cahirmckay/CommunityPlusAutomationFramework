using CommunityPlusAutomation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityPlusAutomationFramework
{
    public class BasePage : BaseTest
    {
        public BasePage() : base()
        {
            PageFactory.InitElements(driver, this);
        }

        protected T GetPage<T>() where T : BasePage, new()
        {
            T page = new T();

            return page;    
        }

        public void GoToSite()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            if (currentBrowser == "Chrome")
            {
                driver.Navigate().GoToUrl(Constants.URL);
            }
            else
            {
                try
                {
                    driver.Navigate().GoToUrl(Constants.URL);
                }
                catch (Exception ex)
                {
                }
            }

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void ConnectionPass()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            if (currentBrowser == "Chrome")
            {
                driver.WaitUntilElementIsClickable(By.Id("details-button")).Click();

                driver.WaitUntilElementIsClickable(By.Id("proceed-link")).Click();
            }
            else
            {
                driver.WaitUntilElementIsClickable(By.Id("advancedButton")).Click();
                driver.WaitUntilElementIsClickable(By.Id("exceptionDialogButton")).Click();
            }

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void Logout()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            driver.WaitUntilElementIsClickable(By.Id("navbarDropdownMenuLink")).Click();

            driver.WaitUntilElementIsClickable(By.XPath("(//button[@type='submit'][normalize-space()='Logout'])[1]")).Click();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void ScrollToXPath(string elementXpath)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            IWebElement text = driver.FindElement(By.XPath(elementXpath));
            jse.ExecuteScript("arguments[0].scrollIntoView();", text);

            Thread.Sleep(1000);
        }
    }
}
