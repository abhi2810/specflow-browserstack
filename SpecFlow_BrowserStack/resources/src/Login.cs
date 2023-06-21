using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace SpecFlowBrowserStack
{
    [Binding]
    public class Login
    {
        private readonly IWebDriver _driver;
        readonly WebDriverWait wait;

        public Login()
        {
            _driver = BrowserStackSpecFlowTest.ThreadLocalDriver.Value;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        [Then(@"I click on Sign In link")]
        public void ThenIClickOnSignInLink()
        {
            _driver.FindElement(By.Id("signin")).Click();
        }

        [When(@"I type '(.*)' in username")]
        public void ITypeUsername(string username)
        {
            wait.Until(ExpectedConditions.ElementExists(By.Id("react-select-2-input")));
            _driver.FindElement(By.Id("react-select-2-input")).SendKeys(username);
            _driver.FindElement(By.Id("react-select-2-input")).SendKeys(Keys.Enter);
        }
        [When(@"I type '(.*)' in password")]
        public void ITypePassword(string password)
        {
            _driver.FindElement(By.Id("react-select-3-input")).SendKeys(password);
            _driver.FindElement(By.Id("react-select-3-input")).SendKeys(Keys.Enter);
        }

        [Then(@"I press Log In Button")]
        public void IPressLogInButton()
        {
            _driver.FindElement(By.Id("login-btn")).Click();
        }
        [Then(@"I should see user '(.*)' logged in")]
        public void IshouldSeeUsername(string username)
        {
            if (username == "locked_user")
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//h3[@class='api-error']")));
                string errorMsg = _driver.FindElement(By.XPath("//h3[@class='api-error']")).Text;
                Assert.AreEqual(errorMsg, "Your account has been locked.");

            }
            else
            {
                wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[@class='username']")));
                string displayedUsername = _driver.FindElement(By.XPath("//span[@class='username']")).Text;
                Assert.AreEqual(username, displayedUsername);

            }
        }
    }
}
