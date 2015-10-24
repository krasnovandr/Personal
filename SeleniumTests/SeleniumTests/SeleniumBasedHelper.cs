using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace SeleniumTests
{
    class SeleniumBasedHelper
    {
        private IWebDriver _driver;
        private Actions _actions;

        public void SetUpSelenium()
        {
            _driver = new FirefoxDriver();

            if (_driver.GetType() == typeof (FirefoxDriver))
            {
                _driver.Manage().Window.Maximize();
            }
            if (_driver.GetType() == typeof (ChromeDriver))
            {
                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                _driver = new ChromeDriver(options);
            }

            _driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 0, 5));
            _actions = new Actions(_driver);
        }

        public void GotoUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }



        public IWebElement FindElementByXpath(string xpathSelector)
        {
            return _driver.FindElement(By.XPath(xpathSelector));
        }

        public IWebElement FindElementByLinkText(string linkText)
        {
            //    driver.FindElement(By.LinkText("Расписание групп")).Click();
            return _driver.FindElement(By.LinkText(linkText));
        }

        public void MoveToElementAndClick(IWebElement menuHoverLink)
        {
            _actions.MoveToElement(menuHoverLink)
                .MoveByOffset(menuHoverLink.Location.X, menuHoverLink.Location.Y)
                .Click(menuHoverLink)
                .Build()
                .Perform();
        }

        public IWebElement FindByCss(string selector)
        {
            return _driver.FindElement(By.CssSelector(selector));
        }

        public void ClickByElement(IWebElement element)
        {
            element.Click();
        }

        public void ClearElement(IWebElement element)
        {
            element.Clear();
        }

        public void Type(IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        public IWebElement FindElementById(string id)
        {
            return _driver.FindElement(By.Id(id));
        }

        public IWebElement FindElementByName(string name)
        {
            return _driver.FindElement(By.Name(name));
        }
    }
}
