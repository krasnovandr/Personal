using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private string shedulePageUrl;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://www.bsuir.by/";
            verificationErrors = new StringBuilder();
            shedulePageUrl = "/online/showpage.jsp?PageID=94599&resID=100229&lang=ru&menuItemID=101492";
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void SearchGroupSchedule()
        {
           driver.Navigate().GoToUrl(baseURL + shedulePageUrl);
            
            Actions actions = new Actions(driver);
            var menuHoverLink = driver.FindElement(By.LinkText("Студентам"));
            actions.MoveToElement(menuHoverLink);
            //driver.Navigate(menuHoverLink)moveToElement(menuHoverLink);
           // driver.Navigate().GoToUrl(baseURL + shedulePageUrl);
            driver.FindElement(By.LinkText("Расписание")).Click();
            driver.FindElement(By.LinkText("Расписание групп")).Click();
            driver.FindElement(By.Id("studentGroupTab:studentGroupForm:searchStudentGroup")).Clear();
            driver.FindElement(By.Id("studentGroupTab:studentGroupForm:searchStudentGroup")).SendKeys("533701");
            driver.FindElement(By.Id("studentGroupTab:studentGroupForm:j_idt21")).Click();
            Assert.AreEqual("Расписание для группы 533701", driver.FindElement(By.XPath("//*[@id=\"tableForm:schedulePanel\"]/span")).Text);
        }

        [Test]
        public void SearchTeachersSchedule()
        {
            driver.Navigate().GoToUrl(baseURL + shedulePageUrl);
            driver.FindElement(By.LinkText("Расписание преподавателей")).Click();
            driver.FindElement(By.Id("employeeForm:searchEmployee_input")).Clear();
            driver.FindElement(By.Id("employeeForm:searchEmployee_input")).SendKeys("Перцев Дмитрий Юрьевич");
            driver.FindElement(By.Id("employeeForm:j_idt21")).Click();
            Assert.AreEqual("Расписание для Перцев Дмитрий Юрьевич", driver.FindElement(By.XPath("//*[@id=\"tableForm:schedulePanel\"]/span")).Text);
        }

        [Test]
        public void FeedbackValidationRequiredTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Обратная связь")).Click();
            driver.FindElement(By.Name("newprop_14537_1")).Clear();
            driver.FindElement(By.Name("newprop_14537_1")).SendKeys("Андрей");
            driver.FindElement(By.Name("newprop_14538_1")).Clear();
            driver.FindElement(By.Name("newprop_14538_1")).SendKeys("Контактная информация");
            driver.FindElement(By.Name("save")).Click();
            Assert.AreEqual("Неправильно заполненная форма.", driver.FindElement(By.ClassName("incorrect_input_form ")).Text);
        
            // ERROR: Caught exception [Error: unknown strategy [class] for locator [class=incorrect_input_form]]
            // ERROR: Caught exception [Error: unknown strategy [class] for locator [class=incorrect_values]]
        }

        [Test]
        public void RecoverFeedbackInformation()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Обратная связь")).Click();
            driver.FindElement(By.Name("newprop_14537_1")).Clear();
            driver.FindElement(By.Name("newprop_14537_1")).SendKeys("Андрей");
            driver.FindElement(By.Name("newprop_14538_1")).Clear();
            driver.FindElement(By.Name("newprop_14538_1")).SendKeys("Контактная информация");
            driver.FindElement(By.Name("save")).Click();
            driver.FindElement(By.Name("newprop_14538_1")).Clear();
            driver.FindElement(By.Name("newprop_14538_1")).SendKeys("");
            driver.FindElement(By.Name("newprop_14537_1")).Clear();
            driver.FindElement(By.Name("newprop_14537_1")).SendKeys("");
            driver.FindElement(By.XPath("//input[@value='Восстановить']")).Click();
            Assert.AreEqual("Андрей", driver.FindElement(By.Name("newprop_14537_1")).Text);
            Assert.AreEqual("Контактная информация", driver.FindElement(By.Name("newprop_14538_1")).Text);
        }


    }
}

