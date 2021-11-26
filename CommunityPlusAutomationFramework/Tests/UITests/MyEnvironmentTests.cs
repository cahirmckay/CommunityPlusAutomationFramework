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
    public class MyEnvironmentTests : BaseTest
    {
        [Description("Adding an Issue")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\MyEnvironmentTest.xml",
            @"CreateAnIssue",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void CreateAnIssue()
        {
            logger.Info("\nEntering CreateAnIssue Method");

            // Open Browser and Navigate to Community Plus HomePage
            var homePage = new HomePage();
            homePage.GoToSite();

            // Bypass connection issue (Will be removed when site is live)
            homePage.ConnectionPass();

            // Click Login Menu Item
            LoginPage loginPage = homePage.ClickMenuItem<LoginPage>(MenuItems.Login);

            // Login as Admin
            AdminHomePage adminHomePage = loginPage.Login<AdminHomePage>(Constants.AdminEmail, Constants.AdminPassword);

            // Click the Dashboard Menu item
            DashboardPage dashboardPage = adminHomePage.ClickMenuItem<DashboardPage>(MenuItems.DashBoard);

            // Click the MyEnvironment button
            MyEnvironmentPage myEnvironmentPage = dashboardPage.ClickFeatureButton<MyEnvironmentPage>(Features.MyEnvironment);

            // Click the Add an issue button
            CreateAnIssuePage createAnIssuePage = myEnvironmentPage.ClickButtonById<CreateAnIssuePage>("AddAnIssue");

            // Add a description
            createAnIssuePage.EnterTextboxArea("Description", "Bad litter problem here");

            // Add Latitude
            createAnIssuePage.EnterTextbox("Latitude", "54.9966");

            // Add Longitude
            createAnIssuePage.EnterTextbox("Longitude", "-7.3086");

            // Click Create button
            myEnvironmentPage =  createAnIssuePage.ClickButton<MyEnvironmentPage>("Create");

            // Assert issue has been created
            IWebElement issueCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(issueCreated.Text.Equals("Your issue has been successfully added"));

            // Logout as Admin
            adminHomePage.Logout();

            logger.Info("\nExiting CreateAnIssue Method");
        }
    }
}
