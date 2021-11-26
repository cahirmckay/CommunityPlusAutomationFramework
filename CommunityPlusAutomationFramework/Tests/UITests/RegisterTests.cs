using CommunityPlusAutomation;
using CommunityPlusAutomationFramework.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPlusAutomationFramework
{
    [TestClass]
    public class RegisterTests : BaseTest
    {
        [Description("Register a user")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\RegisterTest.xml",
            @"RegisterAUser",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void RegisterAUser()
        {
            logger.Info("\nEntering RegisterAUser Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Register Menu Item
            RegisterPage registerPage = homePage.ClickMenuItem<RegisterPage>(MenuItems.Register);

            // Enter Credentials
            registerPage.EnterTextbox("Name", "Joe");
            registerPage.EnterTextbox("Age", "25");
            registerPage.EnterTextbox("Gender", "Male");

            if (currentBrowser == "Chrome")
            {
                registerPage.EnterTextbox("Email", "joe1@mail.com");
            }
            else
            {
                registerPage.EnterTextbox("Email", "joe2@mail.com");
            }

            registerPage.EnterTextbox("CommunityId", "1");
            registerPage.EnterTextbox("Password", "joe");
            registerPage.EnterTextbox("Password Confirm", "joe");

            //Click Register
            LoginPage loginPage = registerPage.ClickButton<LoginPage>("Register");

            // Assert you have been registered successfully
            IWebElement alert = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-info'])[1]"));
            Assert.IsTrue(alert.Text.Equals("Successfully Registered. Now login"));

            logger.Info("\nExiting RegisterAUser Method");
        }

        [Description("Negative test for Registering a user")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\RegisterTest.xml",
            @"RegisterAnExistingUser",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void RegisterAUserNegativeTest()
        {
            logger.Info("\nEntering RegisterAnExistingUser Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Register Menu Item
            RegisterPage registerPage = homePage.ClickMenuItem<RegisterPage>(MenuItems.Register);

            // Enter Credentials
            registerPage.EnterTextbox("Name", "Joe");
            registerPage.EnterTextbox("Age", "25");
            registerPage.EnterTextbox("Gender", "Male");

            if (currentBrowser == "Chrome")
            {
                registerPage.EnterTextbox("Email", "joe3@mail.com");
            }
            else
            {
                registerPage.EnterTextbox("Email", "joe4@mail.com");
            }

            registerPage.EnterTextbox("CommunityId", "1");
            registerPage.EnterTextbox("Password", "joe");
            registerPage.EnterTextbox("Password Confirm", "joe");

            //Click Register
            LoginPage loginPage = registerPage.ClickButton<LoginPage>("Register");

            // Assert you have been registered successfully
            IWebElement alert = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-info'])[1]"));
            Assert.IsTrue(alert.Text.Equals("Successfully Registered. Now login"));

            registerPage = loginPage.ClickMenuItem<RegisterPage>(MenuItems.Register);

            // Enter Credentials
            registerPage.EnterTextbox("Name", "Joe");
            registerPage.EnterTextbox("Age", "25");
            registerPage.EnterTextbox("Gender", "Male");

            if (currentBrowser == "Chrome")
            {
                registerPage.EnterTextbox("Email", "joe3@mail.com");
            }
            else
            {
                registerPage.EnterTextbox("Email", "joe4@mail.com");
            }

            registerPage.EnterTextbox("CommunityId", "1");
            registerPage.EnterTextbox("Password", "joe");
            registerPage.EnterTextbox("Password Confirm", "joe");

            //Click Register
            registerPage.ClickButton<RegisterPage>("Register");

            // Assert email already exists
            IWebElement emailExists = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[1]"));

            if (currentBrowser == "Chrome")
            {
                Assert.IsTrue(emailExists.Text.Equals("A user with this email address joe3@mail.com already exists."));
            }
            else
            {
                Assert.IsTrue(emailExists.Text.Equals("A user with this email address joe4@mail.com already exists."));
            }

            // Enter correct email
            if (currentBrowser == "Chrome")
            {
                registerPage.EnterTextbox("Email", "joe5@mail.com");
            }
            else
            {
                registerPage.EnterTextbox("Email", "joe6@mail.com");
            }
            // Enter Password
            registerPage.EnterTextbox("Password", "joe");

            // Enter different confirm password
            registerPage.EnterTextbox("Password Confirm", "joe1");

            //Click Register
            registerPage.ClickButton<RegisterPage>("Register");

            // Assert different Passwords
            IWebElement differentPassword = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[1]"));
            Assert.IsTrue(differentPassword.Text.Equals("Confirm password doesn't match, Type again !"));

            RefreshPage();

            //Click Register
            registerPage.ClickButton<RegisterPage>("Register");

            // Assert blank Name
            IWebElement blankName = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[1]"));
            Assert.IsTrue(blankName.Text.Equals("The Name field is required."));

            // Assert blank Age
            IWebElement blankAge = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[2]"));
            Assert.IsTrue(blankAge.Text.Equals("The Age field is required."));

            // Assert blank Gender
            IWebElement blankGender = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[3]"));
            Assert.IsTrue(blankGender.Text.Equals("The Gender field is required."));

            // Assert blank Email
            IWebElement blankEmail = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[4]"));
            Assert.IsTrue(blankEmail.Text.Equals("The Email field is required."));

            // Assert blank CommunityId
            IWebElement blankCommunityId = driver.WaitUntilElementIsVisible(By.XPath("(//span[@class='text-danger field-validation-error'])[5]"));
            Assert.IsTrue(blankCommunityId.Text.Equals("The CommunityId field is required."));

            logger.Info("\nExiting RegisterAnExistingUser Method");
        }
    }
}
