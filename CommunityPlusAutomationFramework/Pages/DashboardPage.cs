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
    public class DashboardPage : ViewPanel
    {
        public T ClickFeatureButton<T>(Features feature) where T : BasePage, new()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            driver.WaitUntilElementIsClickable(By.XPath("(//*[@class='btn btn-primary'][normalize-space()='" + feature + "'])[1]")).Click();

            return GetPage<T>();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }
    }
}
