using CommunityPlusAutomation;
using CommunityPlusAutomationFramework.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityPlusAutomationFramework
{
    [TestClass]
    public class LoginTests : BaseTest
    {

        [Description("Logging in as Admin user")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\LoginTest.xml",
            @"AdminLogin",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void AdminLogin()
        {
            logger.Info("\nEntering AdminLogin Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Login Menu Item
            LoginPage loginPage = homePage.ClickMenuItem<LoginPage>(MenuItems.Login);

            // Login as Admin
            AdminHomePage adminHomePage = loginPage.Login<AdminHomePage>(Constants.AdminEmail, Constants.AdminPassword);

            // Assert you have been logged in successfully
            IWebElement alert = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-info'])[1]"));
            Assert.IsTrue(alert.Text.Equals("Successfully Logged in"));
       
            // Assert Community data is visible
            IWebElement communityWelcome = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='card-title'])[1]"));
            Assert.IsTrue(communityWelcome.Text.Equals("Welcome to Kilrea Community"));
            
            // Logout as Admin
            adminHomePage.Logout();

            // Assert you have logged out successfully
            IWebElement loginHeader = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='mb-3'])[1]"));
            Assert.IsTrue(loginHeader.Text.Equals("Login"));

            logger.Info("\nExiting AdminLogin Method");
        }

        [Description("Logging in as Guest user")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
           "|DataDirectory|\\Tests\\Data\\LoginTest.xml",
           @"GuestLogin",
           DataAccessMethod.Sequential)]
        [TestMethod]
        public void GuestLogin()
        {
            logger.Info("\nEntering GuestLogin Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Login Menu Item
            LoginPage loginPage = homePage.ClickMenuItem<LoginPage>(MenuItems.Login);

            // Login as Guest
            AdminHomePage adminHomePage = loginPage.Login<AdminHomePage>("harry@mail.com", "pw");

            // Assert you have been logged in successfully
            IWebElement alert = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-info'])[1]"));
            Assert.IsTrue(alert.Text.Equals("Successfully Logged in"));

            // Assert Community data is visible
            IWebElement communityWelcome = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='card-title'])[1]"));
            Assert.IsTrue(communityWelcome.Text.Equals("Welcome to Kilrea Community"));

            // Logout as Guest
            adminHomePage.Logout();

            // Assert you have logged out successfully
            IWebElement loginHeader = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='mb-3'])[1]"));
            Assert.IsTrue(loginHeader.Text.Equals("Login"));

            logger.Info("\nExiting GuestLogin Method");
        }

        [Description("Logging in as manager user")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
           "|DataDirectory|\\Tests\\Data\\LoginTest.xml",
           @"ManagerLogin",
           DataAccessMethod.Sequential)]
        [TestMethod]
        public void ManagerLogin()
        {
            logger.Info("\nEntering ManagerLogin Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Login Menu Item
            LoginPage loginPage = homePage.ClickMenuItem<LoginPage>(MenuItems.Login);

            // Login as Manager
            AdminHomePage adminHomePage = loginPage.Login<AdminHomePage>("molly@mail.com", "man");

            // Assert you have been logged in successfully
            IWebElement alert = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-info'])[1]"));
            Assert.IsTrue(alert.Text.Equals("Successfully Logged in"));

            // Assert Community data is visible
            IWebElement communityWelcome = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='card-title'])[1]"));
            Assert.IsTrue(communityWelcome.Text.Equals("Welcome to Kilrea Community"));

            // Logout as Manager
            adminHomePage.Logout();

            // Assert you have logged out successfully
            IWebElement loginHeader = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='mb-3'])[1]"));
            Assert.IsTrue(loginHeader.Text.Equals("Login"));

            logger.Info("\nExiting ManagerLogin Method");
        }

        [Description("Logging in as manager user")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
           "|DataDirectory|\\Tests\\Data\\LoginTest.xml",
           @"IncorrectLogin",
           DataAccessMethod.Sequential)]
        [TestMethod]
        public void IncorrectLogin()
        {
            logger.Info("\nEntering IncorrectLogin Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Login Menu Item
            LoginPage loginPage = homePage.ClickMenuItem<LoginPage>(MenuItems.Login);

            // Login as Manager with incorrect password
            loginPage.Login<LoginPage>("molly@mail.com", "woman");

            // Assert you cannot log in with incorrect credentials
            IWebElement invalidEmail = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='text-danger field-validation-error'])[1]"));
            Assert.IsTrue(invalidEmail.Text.Equals("Invalid Login Credentials"));

            logger.Info("\nExiting IncorrectLogin Method");
        }
    }
}
