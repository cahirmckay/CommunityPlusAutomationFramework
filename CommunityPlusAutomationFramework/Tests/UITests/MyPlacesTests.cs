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
    public class MyPlacesTests : BaseTest
    {
        [Description("Adding a headline")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\MyPlacesTest.xml",
            @"AddingABusiness",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void AddingABusiness()
        {
            logger.Info("\nEntering AddingABusiness Method");

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

            // Scroll to My Photos button
            dashboardPage.ScrollToXPath("(//*[@class='btn btn-primary'][normalize-space()='MyPhotos'])[1]");

            // Click the MyPlaces button
            MyPlacesPage myPlacesPage = dashboardPage.ClickFeatureButton<MyPlacesPage>(Features.MyPlaces);

            // Click the Add a business
            CreateABusinessPage createABusinessPage = myPlacesPage.ClickButtonById<CreateABusinessPage>("CreateABusiness");

            // Add a title
            createABusinessPage.EnterTextbox("Title", "Peters");

            // Add a type
            createABusinessPage.EnterTextbox("Type", "Bar");

            // Add an address
            createABusinessPage.EnterTextbox("Address", "1 maghera road");

            // Add a description
            createABusinessPage.EnterTextboxArea("Description", "beerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeerbeer");

            // Add a community id
            createABusinessPage.EnterTextbox("Community Id", "1");

            // Add a posterurl
            createABusinessPage.EnterTextboxById("PosterUrl", "https://thelakekilrea.com/wp-content/uploads/2021/03/photo-11-08-2020-11-30-22-scaled.jpg");

            // Click Create 
            myPlacesPage = createABusinessPage.ClickButton<MyPlacesPage>("Create");

            // Assert business has been created
            IWebElement businessCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(businessCreated.Text.Equals("Peters has been successfully added"));

            // Click view on business
            myPlacesPage.ClickViewButton("Peters");

            // Click Add review
            AddReviewPage addReviewPage = myPlacesPage.ClickButtonById<AddReviewPage>("AddReview");

            // Add comment 
            addReviewPage.EnterTextbox("Comment", "really really good beer");

            // Add Rating 
            addReviewPage.EnterTextbox("Rating", "10");

            // Click Add
            myPlacesPage = addReviewPage.ClickButton<MyPlacesPage>("Add");

            // Assert review has been created
            IWebElement reviewCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(reviewCreated.Text.Equals("Review of Peters has been added"));

            // Logout as Admin
            adminHomePage.Logout();

            logger.Info("\nExiting AddingABusiness Method");
        }
    }
}
