using OpenQA.Selenium;
using SauceDemoUITests.Drivers;
using SauceDemoUITests.Pages;
using FluentAssertions;
using NLog;
using NLog.Config;

namespace SauceDemoUITests.Tests
{
    [TestClass]
    public class LoginTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private IWebDriver? driver;
        private LoginPage? loginPage;

        [ClassInitialize]
        public static void SetupLogger(TestContext context)
        {
            var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");

            if (File.Exists(configFilePath))
            {
                LogManager.Configuration = new XmlLoggingConfiguration(configFilePath);
                Logger.Info("NLog initialized successfully.");
            }
            else
            {
                Console.WriteLine("NLog.config file not found!");
            }
        }

        [TestMethod]
        [DataRow("chrome")]
        [DataRow("edge")]
        public void TestLoginWithEmptyCredentials(string browser)
        {
            Logger.Info($"Executing TestLoginWithEmptyCredentials on {browser}");

            driver = WebDriverFactory.CreateDriver(browser);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);

            loginPage.ClickLogin();
            string errorText = loginPage.GetErrorMessage();

            errorText.Should().Be("Epic sadface: Username is required");
            Logger.Info($"TestLoginWithEmptyCredentials passed on {browser}");
        }

        [TestMethod]
        [DataRow("chrome")]
        [DataRow("edge")]
        public void TestLoginWithOnlyUsername(string browser)
        {
            Logger.Info($"Executing TestLoginWithOnlyUsername on {browser}");

            driver = WebDriverFactory.CreateDriver(browser);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);

            loginPage.EnterUsername("standard_user");
            loginPage.ClickLogin();
            string errorText = loginPage.GetErrorMessage();

            errorText.Should().Be("Epic sadface: Password is required");
            Logger.Info($"TestLoginWithOnlyUsername passed on {browser}");
        }

        [TestMethod]
        [DataRow("chrome")]
        [DataRow("edge")]
        public void TestLoginWithValidCredentials(string browser)
        {
            Logger.Info($"Executing TestLoginWithValidCredentials on {browser}");

            driver = WebDriverFactory.CreateDriver(browser);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            loginPage = new LoginPage(driver);

            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClickLogin();

            driver.Title.Should().Be("Swag Labs");
            Logger.Info($"TestLoginWithValidCredentials passed on {browser}");
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                Logger.Info("Closing WebDriver");
                driver.Quit();
            }
            else
            {
                Logger.Warn("WebDriver was not initialized, skipping Quit()");
            }
        }
    }
}
