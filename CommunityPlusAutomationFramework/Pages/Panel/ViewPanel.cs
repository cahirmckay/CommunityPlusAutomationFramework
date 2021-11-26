using CommunityPlusAutomation;
using CommunityPlusAutomationFramework.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlusAutomationFramework
{
    public class ViewPanel : BasePage
    {
        public T ClickMenuItem<T>(MenuItems menuItem) where T : BasePage, new()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            driver.WaitUntilElementIsClickable(By.XPath("(//*[@class='nav-link text-dark'][text()='" + menuItem + "'])[1]")).Click();

            return GetPage<T>();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void EnterTextbox(string fieldLabel, string value)
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            IWebElement textbox =  driver.WaitUntilElementIsClickable(By.XPath("(//*[normalize-space()='" + fieldLabel + "']//following::input)[1]"));

            textbox.Clear();
            textbox.SendKeys(value);

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void EnterTextboxArea(string fieldLabel, string value)
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            IWebElement textbox = driver.WaitUntilElementIsClickable(By.XPath("(//*[normalize-space()='" + fieldLabel + "']//following::textarea)[1]"));

            textbox.Clear();
            textbox.SendKeys(value);

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void EnterTextboxById(string id, string value)
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            IWebElement textbox = driver.WaitUntilElementIsClickable(By.Id(id));

            textbox.Clear();
            textbox.SendKeys(value);

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public T ClickButton<T>(string buttonText) where T : BasePage, new()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            driver.WaitUntilElementIsClickable(By.XPath("(//*[@type='submit'][@value='"+ buttonText +"'])[1]")).Click();

            return GetPage<T>();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public T ClickButtonById<T>(string buttonId) where T : BasePage, new()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            driver.WaitUntilElementIsClickable(By.Id(buttonId)).Click();

            return GetPage<T>();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void EnterUploadFile(string fileName, int number = 1)
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            IWebElement chooseFiles = driver.FindElement(By.XPath("(//*[@type='file'])[1]"));

            chooseFiles.SendKeys(Constants.DataLocation + fileName);

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }

        public void ClickViewButton(string name)
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            driver.WaitUntilElementIsClickable(By.XPath("(//*[text()='" + name + "']//following::a[@class='btn btn-primary my-1'][text()='View'])[1]")).Click();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }
    }
}
