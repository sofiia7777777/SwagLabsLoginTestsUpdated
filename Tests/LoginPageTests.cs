using FluentAssertions;
using OpenQA.Selenium;
using Serilog;
using OpenQA.Selenium.Support.UI;
using Core;
using Pages;

namespace Tests
{
    public class LoginPageTests : IDisposable
    {
        protected IWebDriver? driver;
        private bool disposed = false;

        private void InitializeDriver(string browserType)
        {
            driver = BrowserManager.GetDriver(browserType);
        }

        [Theory]
        [MemberData(nameof(BrowserTestData.GetBrowserTestData), MemberType = typeof(BrowserTestData))]
        public void TestLoginFormWithEmptyCredentials_ShowsUsernameRequiredError(string browserType)
        {
            TestLogger.Initialize();
            Log.Information("UC-1: Test Login form with empty credentials");

            Log.Information("Starting test for browser {BrowserType}", browserType);
            InitializeDriver(browserType);
            var loginPage = new LoginPage(driver!);

            Log.Information("Opening the login page.");
            loginPage.Open();

            loginPage.Login("any_username", "any_password");

            Log.Information("Clearing username and password.");
            loginPage.ClearUsername();
            loginPage.ClearPassword();

            Log.Information("Clicking the login button.");
            loginPage.ClickLoginButton();

            string errorMessage = loginPage.GetErrorMessage();
            Log.Information("ErrorMessage: {errorMessage}", errorMessage);

            errorMessage.Should().Be("Epic sadface: Username is required");
            Log.Information("Test completed successfully.");
            Log.CloseAndFlush();
        }


        [Theory]
        [MemberData(nameof(BrowserTestData.GetBrowserTestData), MemberType = typeof(BrowserTestData))]
        public void TestLoginFormWithEmptyPasswordField_ShowsPasswordRequiredError(string browserType)
        {
            TestLogger.Initialize();

            Log.Information("UC-2: Test Login form with credentials by passing Username");

            Log.Information("Starting test for browser {BrowserType}", browserType);
            InitializeDriver(browserType);
            var loginPage = new LoginPage(driver!);

            Log.Information("Opening the login page.");
            loginPage.Open();

            loginPage.Login("any_username", "any_password");

            Log.Information("Clearing password.");
            loginPage.ClearPassword();

            Log.Information("Clicking the login button.");
            loginPage.ClickLoginButton();


            string errorMessage = loginPage.GetErrorMessage();
            Log.Information("ErrorMessage: {errorMessage}", errorMessage);

            errorMessage.Should().Be("Epic sadface: Password is required");
            Log.Information("Test completed successfully.");
            Log.CloseAndFlush();
        }


        [Theory]
        [MemberData(nameof(BrowserTestData.GetBrowserAndUserNameTestData), MemberType = typeof(BrowserTestData))]
        public void TestLoginFormWithCredentials_ShowsSwagLabsTitleInTheDashboard(string browserType, string userName)
        {
            TestLogger.Initialize();

            Log.Information("UC-3 Test Login form with credentials by passing Username & Password");

            Log.Information("Starting test for browser {BrowserType}", browserType);
            InitializeDriver(browserType);
            var loginPage = new LoginPage(driver!);

            Log.Information("Opening the login page.");
            loginPage.Open();

            loginPage.Login(userName, "secret_sauce");

            Log.Information("Clicking the login button.");
            loginPage.ClickLoginButton();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25),
                Message = "Page title has not been found"
            };

            string pageTitle = wait.Until(driver => driver.FindElement(By.ClassName("app_logo"))).Text;
            Log.Information("Title: {pageTitle} in the dashboard", pageTitle);

            pageTitle.Should().Be("Swag Labs");
            Log.Information("Test completed successfully.");
            Log.CloseAndFlush();
        }


        [Theory]
        [MemberData(nameof(BrowserTestData.GetBrowserTestData), MemberType = typeof(BrowserTestData))]
        public void TestLoginFormForLockedOutUser_ShowsUserIsLockedOutError(string browserType)
        {
            TestLogger.Initialize();
            Log.Information("Test Login form for locked_out_user");

            Log.Information("Starting test for browser {BrowserType}", browserType);
            InitializeDriver(browserType);
            var loginPage = new LoginPage(driver!);

            Log.Information("Opening the login page.");
            loginPage.Open();

            loginPage.Login("locked_out_user", "secret_sauce");

            Log.Information("Clicking the login button.");
            loginPage.ClickLoginButton();

            string errorMessage = loginPage.GetErrorMessage();
            Log.Information("ErrorMessage: {errorMessage}", errorMessage);

            errorMessage.Should().Be("Epic sadface: Sorry, this user has been locked out.");
            Log.Information("Test completed successfully.");
            Log.CloseAndFlush();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    driver?.Quit();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}