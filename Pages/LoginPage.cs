using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;

namespace Pages
{
    public class LoginPage
    {
        private static string Url { get; } = "https://www.saucedemo.com/";

        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentException(nameof(driver));

        public LoginPage Open()
        {
            driver.Url = Url;
            return this;
        }

        private IWebElement WaitForElement(By locator)
        {
            var waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25),
                Message = $"Element with locator {locator} has not been found"
            };

            return waitForElement.Until(driver => driver.FindElement(locator));
        }

        public void Login(string userName, string password)
        {
            Log.Information("Entering username.");
            var usernameField = WaitForElement(By.Id("user-name"));
            usernameField.Click();
            usernameField.SendKeys(userName);

            Log.Information("Entering password.");
            var passwordField = WaitForElement(By.Id("password"));
            passwordField.Click();
            passwordField.SendKeys(password);
        }

        public void ClearUsername()
        {
            var usernameField = WaitForElement(By.Id("user-name"));
            usernameField.Click();
            usernameField.SendKeys(Keys.Control + Keys.Backspace);
        }

        public void ClearPassword()
        {
            var passwordField = WaitForElement(By.Id("password"));
            passwordField.Click();
            passwordField.SendKeys(Keys.Control + Keys.Backspace);
        }

        public void ClickLoginButton()
        {
            var loginButton = WaitForElement(By.Id("login-button"));
            loginButton.Click();
        }

        public string GetErrorMessage()
        {
            var errorMessage = WaitForElement(By.CssSelector(".error-message-container"));
            return errorMessage.Text;
        }
    }
}
