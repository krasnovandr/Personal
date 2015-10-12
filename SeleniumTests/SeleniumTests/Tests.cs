using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private IJavaScriptExecutor javaScriptExecutor;
        private StringBuilder verificationErrors;
        private string baseURL;
        private string shedulePageUrl;
        private bool acceptNextAlert = true;
        private Actions actions;
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();

            if (driver.GetType() == typeof(FirefoxDriver))
            {
                driver.Manage().Window.Maximize();
            }
            if (driver.GetType() == typeof(ChromeDriver))
            {
                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                driver = new ChromeDriver(options);
            }
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 0, 5));
            actions = new Actions(driver);
            javaScriptExecutor = driver as IJavaScriptExecutor;
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
            driver.Navigate().GoToUrl(baseURL);
            MoveToShedules();

            driver.FindElement(By.LinkText("Расписание групп")).Click();
            driver.FindElement(By.Id("studentGroupTab:studentGroupForm:searchStudentGroup")).Clear();
            driver.FindElement(By.Id("studentGroupTab:studentGroupForm:searchStudentGroup")).SendKeys("533701");
            driver.FindElement(By.Id("studentGroupTab:studentGroupForm:j_idt21")).Click();
            //Thread.Sleep(5000);
            Assert.AreEqual("Расписание для группы 533701", driver.FindElement(By.XPath("//*[@id=\"tableForm:schedulePanel\"]/span")).Text);
        }

        private void MoveToShedules()
        {
            var menuHoverLink = driver.FindElement(By.CssSelector("#main-menu > li:nth-child(1)"));
            actions.MoveToElement(menuHoverLink)
                .MoveByOffset(menuHoverLink.Location.X, menuHoverLink.Location.Y)
                .Build()
                .Perform();
            driver.FindElement(By.XPath("//a[@href='/online/showpage.jsp?PageID=94599&resID=100229&lang=ru&menuItemID=101492']"))
                .Click();
        }

        private void MoveToContacts()
        {
            var menuHoverLink = driver.FindElement(By.CssSelector("#m6 > a"));
            actions.MoveToElement(menuHoverLink).MoveByOffset(menuHoverLink.Location.X, menuHoverLink.Location.Y).Click(menuHoverLink).Build().Perform();
            driver.FindElement(By.XPath("//a[@href='/online/showpage.jsp?PageID=81055&resID=100229&lang=ru&menuItemID=121298']"))
                .Click();
        }

        [Test]
        public void SearchTeachersSchedule()
        {
            driver.Navigate().GoToUrl(baseURL);
            MoveToShedules();
            driver.FindElement(By.LinkText("Расписание преподавателей")).Click();
            driver.FindElement(By.Id("employeeForm:searchEmployee_input")).Clear();
            driver.FindElement(By.Id("employeeForm:searchEmployee_input")).SendKeys("Перцев Дмитрий Юрьевич");
            driver.FindElement(By.Id("employeeForm:j_idt21")).Click();
            Assert.AreEqual("Расписание для Перцев Дмитрий Юрьевич", driver.FindElement(By.XPath("//*[@id=\"tableForm:schedulePanel\"]/span")).Text);
        }

        [Test]
        public void TestRequiredValidationOnFeedbackPage()
        {
            driver.Navigate().GoToUrl(baseURL);
            MoveToContacts();
            driver.FindElement(By.Name("newprop_14537_1")).Clear();
            driver.FindElement(By.Name("newprop_14537_1")).SendKeys("Андрей");
            driver.FindElement(By.Name("newprop_14538_1")).Clear();
            driver.FindElement(By.Name("newprop_14538_1")).SendKeys("Контактная информация");
            driver.FindElement(By.Name("save")).Click();
            Assert.AreEqual("Неправильно заполненная форма.", driver.FindElement(By.CssSelector(".incorrect_input_form ")).Text);
            Assert.AreEqual("Необходимо заполнить все поля, обязательные для заполнения.", driver.FindElement(By.CssSelector(".incorrect_values ")).Text);

            // ERROR: Caught exception [Error: unknown strategy [class] for locator [class=incorrect_input_form]]
            // ERROR: Caught exception [Error: unknown strategy [class] for locator [class=incorrect_values]]
        }

        [Test]
        public void TestRecoverInformationOnFeedbackPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/");


            MoveToContacts();
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


        [Test]
        public void ElectronicCirculationTestValidation()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            var menuHoverLink = driver.FindElement(By.CssSelector("#m6 > a"));
            actions.MoveToElement(menuHoverLink).MoveByOffset(menuHoverLink.Location.X, menuHoverLink.Location.Y).Click(menuHoverLink).Build().Perform();
            driver.FindElement(By.LinkText("Электронное обращение"))
                .Click();

            //driver.FindElement(By.LinkText("Электронное обращение")).Click();
            // ERROR: Caught exception [Error: locator strategy either id or name must be specified explicitly.]
            driver.FindElement(By.Name("newprop_21002_1")).Clear();
            driver.FindElement(By.Name("newprop_21002_1")).SendKeys("Ул.В.Хоружей");
            driver.FindElement(By.Name("newprop_21003_1")).Clear();
            driver.FindElement(By.Name("newprop_21003_1")).SendKeys("Краснов Андрей Юрьевич");
            driver.FindElement(By.Name("newprop_21001_1")).Clear();
            driver.FindElement(By.Name("newprop_21001_1")).SendKeys("krasnovandr@mail.ru");
            driver.FindElement(By.Name("save")).Click();
            Assert.AreEqual("Неправильно заполненная форма.", driver.FindElement(By.CssSelector(".incorrect_input_form")).Text);
            Assert.AreEqual("Необходимо заполнить все поля, обязательные для заполнения.", driver.FindElement(By.CssSelector("font.incorrect_values")).Text);
            Assert.AreEqual("Изложение сущности обращения", driver.FindElement(By.CssSelector("li > font.incorrect_values")).Text);
        }


        [Test]
        public void TestFacultySearch()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            //driver.FindElement(By.LinkText("Факультеты")).Click();
            var menuHoverLink = driver.FindElement(By.CssSelector("#main-menu > li:nth-child(1)"));
            actions.MoveToElement(menuHoverLink).MoveByOffset(menuHoverLink.Location.X, menuHoverLink.Location.Y).Click(menuHoverLink).Build().Perform();

            driver.FindElement(By.LinkText("Факультеты")).Click();
            driver.FindElement(By.LinkText("Факультет компьютерных систем и сетей")).Click();
            driver.FindElement(By.CssSelector("div.icon_text > a")).Click();
            try
            {
                Assert.AreEqual("Факультет компьютерных систем и сетей", driver.FindElement(By.LinkText("Факультет компьютерных систем и сетей")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            try
            {
                Assert.AreEqual("Корпус №4", driver.FindElement(By.CssSelector("div.building_title")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }

        [Test]
        public void EumkdSearch()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            var menuHoverLink = driver.FindElement(By.CssSelector("#main-menu > li:nth-child(2)"));
            actions.MoveToElement(menuHoverLink).MoveByOffset(menuHoverLink.Location.X, menuHoverLink.Location.Y).Click(menuHoverLink).Build().Perform();

            driver.FindElement(By.LinkText("ЭРУД (ЭУМКД")).Click();
            driver.FindElement(By.Name("obj_3_1")).Clear();
            driver.FindElement(By.Name("obj_3_1")).SendKeys("Навроцкая И.В.");
            driver.FindElement(By.Name("obj_4_1")).Clear();
            driver.FindElement(By.Name("obj_4_1")).SendKeys("Беларуская мова : культура маўлення");
            driver.FindElement(By.CssSelector("td.search_submit_td > input[type=\"submit\"]")).Click();

            Assert.AreEqual("1 - 1 (1)", driver.FindElement(By.XPath("//table//tr[1]/td[1]")).Text);
            // ERROR: Caught exception [ERROR: Unsupported command [getTable | //td[2]/table[2].0.0 | ]]
        }


    }
}

