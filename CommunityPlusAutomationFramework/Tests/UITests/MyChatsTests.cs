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
    public class MyChatsTests : BaseTest
    {
        [Description("Adding a headline")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\MyChatsTest.xml",
            @"AddingAPost",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void AddingAPost()
        {
            logger.Info("\nEntering AddingAPost Method");

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

            // Scroll to My Chats button
            dashboardPage.ScrollToXPath("(//*[@class='btn btn-primary'][normalize-space()='MyChats'])[1]");

            // Click the MyChats button
            MyChatsPage myChatsPage = dashboardPage.ClickFeatureButton<MyChatsPage>(Features.MyChats);

            // Click join the conversation button
            CreateAPost createAPost =  myChatsPage.ClickButtonById<CreateAPost>("CreateAPost");

            // Add Posttext
            createAPost.EnterTextboxArea("PostText", "Help my cat is stuck in my dishwasher");

            // Click Create
            myChatsPage = createAPost.ClickButton<MyChatsPage>("Create");

            // Assert post has been created
            IWebElement postCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(postCreated.Text.Equals("Your post has been successfully added"));

            // Click view button for post
            myChatsPage.ClickViewButton("Help my cat is stuck in my dishwasher");

            //Click add comment
            AddCommentPage addCommentPage = myChatsPage.ClickButtonById<AddCommentPage>("AddComment");

            // Add Description
            addCommentPage.EnterTextboxArea("Description", "Have you used fairy liquid to pull it out?");

            // Click Add 
            myChatsPage = addCommentPage.ClickButton<MyChatsPage>("Add");

            // Assert comment has been created
            IWebElement commentCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(commentCreated.Text.Equals("Comment by Administrator has been added"));

            // Logout as Admin
            adminHomePage.Logout();

            logger.Info("\nExiting AddingAPost Method");
        }
    }
}
