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
    public class MyPhotosTests : BaseTest
    {
        [Description("Adding a Photo")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\MyPhotosTest.xml",
            @"AddAPhoto",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void AddAPhoto()
        {
            logger.Info("\nEntering AddAPhoto Method");

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

            // Click the MyEnvironment button
            MyPhotosPage myPhotosPage = dashboardPage.ClickFeatureButton<MyPhotosPage>(Features.MyPhotos);

            // Click the create button
            CreateAPhotoPage createAPhotoPage = myPhotosPage.ClickButtonById<CreateAPhotoPage>("CreatePhoto");

            // Add a title
            createAPhotoPage.EnterTextbox("Title", "logo");

            // Add a description
            createAPhotoPage.EnterTextboxArea("Description", "photo description");

            // Add the photo
            createAPhotoPage.EnterUploadFile("photo1.png");

            // Click Create
            myPhotosPage = createAPhotoPage.ClickButton<MyPhotosPage>("Create");

            // Assert issue has been created
            IWebElement issueCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(issueCreated.Text.Equals("logo has been successfully added"));

            // Logout as Admin
            adminHomePage.Logout();

            logger.Info("\nExiting AddAPhoto Method");
        }
    }
}
