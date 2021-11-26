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
    public class MyEventsTests : BaseTest
    {
        [Description("Adding a headline")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\MyEventsTest.xml",
            @"AddingVenueAndEvent",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void AddingVenueAndEvent()
        {
            logger.Info("\nEntering AddingVenueAndEvent Method");

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

            // Scroll to My Events button
            dashboardPage.ScrollToXPath("(//*[@class='btn btn-primary'][normalize-space()='MyEvents'])[1]");

            // Click the MyEvents button
            MyEventsPage myEventsPage = dashboardPage.ClickFeatureButton<MyEventsPage>(Features.MyEvents);

            // Add a Venue
            CreateVenuePage createVenuePage = myEventsPage.ClickButtonById<CreateVenuePage>("AddVenue");

            // Add Name
            createVenuePage.EnterTextbox("Venue Name", "Hotel");

            // Add Address
            createVenuePage.EnterTextbox("Address", "1 maghera lane");

            // Add guidelines
            createVenuePage.EnterTextbox("Current social disancing guidlines (m)", "2");

            // Add Capacity
            createVenuePage.EnterTextbox("Regular capacity(pre-COVID)", "2000");

            // Add Description
            createVenuePage.EnterTextboxArea("Description", "really really cool, great staff, definitely best place to drink alcohol and beers");

            // Click Create
            myEventsPage = createVenuePage.ClickButton<MyEventsPage>("Create");

            // Assert event has been created
            IWebElement eventCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(eventCreated.Text.Equals("Hotel has been successfully added"));

            // View created event 
            myEventsPage.ClickViewButton("Hotel");

            // Logout as Admin
            adminHomePage.Logout();

            logger.Info("\nExiting AddingVenueAndEvent Method");
        }
    }
}
