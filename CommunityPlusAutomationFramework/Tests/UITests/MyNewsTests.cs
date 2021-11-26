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
    public class MyNewsTests : BaseTest
    {
        [Description("Adding a headline")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\Tests\\Data\\MyNewsTest.xml",
            @"AddingAHeadline",
            DataAccessMethod.Sequential)]
        [TestMethod]
        public void AddingAHeadline()
        {
            logger.Info("\nEntering AddingAHeadline Method");

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
            MyNewsPage myNewsPage = dashboardPage.ClickFeatureButton<MyNewsPage>(Features.MyNews);

            // Click the Add an article
            CreateNewsArticlePage createNewsArticlePage = myNewsPage.ClickButtonById<CreateNewsArticlePage>("AddNewsArticleLink");

            // Add a source
            createNewsArticlePage.EnterTextbox("Source", "irish news");

            // Add a Headline
            createNewsArticlePage.EnterTextbox("Headline", "man steals bike");

            // Add ArticleUrl
            createNewsArticlePage.EnterTextbox("ArticleUrl", "https://www.derrynow.com/news/county-derry-post/493268/politician-inundated-with-complaints-on-kilrea-traffic-issues.html");

            // Click create button
            myNewsPage = createNewsArticlePage.ClickButton<MyNewsPage>("Create");

            // Assert headline has been created
            IWebElement headlineCreated = driver.WaitUntilElementIsVisible(By.XPath("(//*[@class='alert alert-success'])[1]"));
            Assert.IsTrue(headlineCreated.Text.Equals("This article has been successfully added"));

            // Logout as Admin
            adminHomePage.Logout();

            logger.Info("\nExiting AddingAHeadline Method");
        }
    }
}
