using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityPlusAutomationFramework
{
    public class LoginPage : ViewPanel
    {
        public T Login<T>(string email, string password) where T : BasePage, new()
        {
            logger.Info("Entering " + MethodBase.GetCurrentMethod() + " Method.");

            EnterTextbox("Email", email);
            EnterTextbox("Password", password);

            IWebElement loginButton = driver.WaitUntilElementIsClickable(By.XPath("(//*[@type='submit'][@value='Login'])[1]"));

            Thread.Sleep(1000);

            loginButton.Click();

            return GetPage<T>();

            logger.Info("Exiting " + MethodBase.GetCurrentMethod() + " Method.");
        }
    }
}
