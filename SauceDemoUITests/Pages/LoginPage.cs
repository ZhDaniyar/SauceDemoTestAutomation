using OpenQA.Selenium;

namespace SauceDemoUITests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        private readonly By usernameField = By.Id("user-name");
        private readonly By passwordField = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.XPath("//h3[@data-test='error']");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void EnterUsername(string username) => driver.FindElement(usernameField).SendKeys(username);
        public void EnterPassword(string password) => driver.FindElement(passwordField).SendKeys(password);
        public void ClickLogin() => driver.FindElement(loginButton).Click();
        public string GetErrorMessage()
        {
            return driver.FindElements(errorMessage).Count > 0
                ? driver.FindElement(errorMessage).Text
                : string.Empty;
        }
        public void ClearFields()
        {
            driver.FindElement(usernameField).Clear();
            driver.FindElement(passwordField).Clear();
        }
    }
}
