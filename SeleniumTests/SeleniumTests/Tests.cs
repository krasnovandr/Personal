using NUnit.Framework;

namespace SeleniumTests
{
    [TestFixture]
    public class Tests
    {
        private BsuirBasedHelper _bsuirBasedHelper;
        private SeleniumBasedHelper _seleniumBasedHelper;

        [TestFixtureSetUp]
        public void SetupTest()
        {
            _seleniumBasedHelper = new SeleniumBasedHelper();
            _seleniumBasedHelper.SetUpSelenium();
            _bsuirBasedHelper = new BsuirBasedHelper(_seleniumBasedHelper);
        }

        [Test]
        [TestCase("533701")]
        public void SearchGroupSchedule(string group)
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.MoveToShedules();
            _bsuirBasedHelper.SelectGroupSchedule(group);

            Assert.AreEqual("Расписание для группы " + group,
                _seleniumBasedHelper.FindElementByXpath("//*[@id=\"tableForm:schedulePanel\"]/span").Text);
        }

        [Test]
        [TestCase("Перцев Дмитрий Юрьевич")]
        public void SearchTeachersSchedule(string teacher)
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.MoveToShedules();
            _bsuirBasedHelper.SelectTeacherSchedule(teacher);

            Assert.AreEqual("Расписание для " + teacher,
                _seleniumBasedHelper.FindElementByXpath(("//*[@id=\"tableForm:schedulePanel\"]/span")).Text);
        }

        [Test]
        public void TestRequiredValidationOnFeedbackPage()
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.MoveToContacts();
            _bsuirBasedHelper.FillContactForm();

            Assert.AreEqual("Неправильно заполненная форма.",
                _seleniumBasedHelper.FindByCss(".incorrect_input_form").Text);
            Assert.AreEqual("Необходимо заполнить все поля, обязательные для заполнения.",
                _seleniumBasedHelper.FindByCss(".incorrect_values").Text);
        }

        [Test]
        public void TestRecoverInformationOnFeedbackPage()
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.MoveToContacts();
            _bsuirBasedHelper.FillContactForm();

            _bsuirBasedHelper.ClearContactForm();

            Assert.AreEqual("Андрей",
                _seleniumBasedHelper.FindElementByName(("newprop_14537_1")).Text);
            Assert.AreEqual("Контактная информация", _seleniumBasedHelper.FindElementByName(("newprop_14538_1")).Text);
        }


        [Test]
        public void ElectronicCirculationTestValidation()
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.MoveToEapplications();

            _bsuirBasedHelper.ElectronicCirculationFillForm();

            Assert.AreEqual("Неправильно заполненная форма.",
                _seleniumBasedHelper.FindByCss(".incorrect_input_form").Text);
            Assert.AreEqual("Необходимо заполнить все поля, обязательные для заполнения.",
                _seleniumBasedHelper.FindByCss("font.incorrect_values").Text);
            Assert.AreEqual("Изложение сущности обращения",
                _seleniumBasedHelper.FindByCss("li > font.incorrect_values").Text);
        }


        [Test]
        public void TestFacultySearch()
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.FacultySearch();

            Assert.AreEqual("Факультет компьютерных систем и сетей",
                   _seleniumBasedHelper.FindElementByLinkText("Факультет компьютерных систем и сетей").Text);
            Assert.AreEqual("Корпус №4", _seleniumBasedHelper.FindByCss("div.building_title").Text);
        }

        [Test]
        [TestCase("Навроцкая И.В.", "Беларуская мова : культура маўлення")]
        public void EumkdSearch(string teacher, string title)
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.BaseUrl);
            _bsuirBasedHelper.MoveToStudentLink();
            _bsuirBasedHelper.SearchEumkd(teacher, title);

            Assert.IsNotEmpty(_seleniumBasedHelper.FindElementByXpath(("//table[@rules='cols']")).Text);
            StringAssert.Contains("1 - 1 (1)", _seleniumBasedHelper.FindElementByXpath("//table[@rules='cols']").Text);
            StringAssert.Contains("Навроцкая И.В.", _seleniumBasedHelper.FindElementByXpath("//table[@rules='cols']").Text);
        }

        [Test]
        [TestCase("Andrei_Krasnou", "3323876")]
        public void ForumAuthorization(string login,string password)
        {
            _bsuirBasedHelper.GotoUrl(_bsuirBasedHelper.ForumUrl);
            _bsuirBasedHelper.LoginToForum(login, password);
           
            Assert.AreEqual("Выход [ Andrei_Krasnou ]",
                _seleniumBasedHelper.FindElementByLinkText("Выход [ Andrei_Krasnou ]").Text);

            _seleniumBasedHelper.FindElementByLinkText("Выход [ Andrei_Krasnou ]").Click();
        }

    }
}

