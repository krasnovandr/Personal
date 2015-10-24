namespace SeleniumTests
{
    class BsuirBasedHelper
    {
        private readonly SeleniumBasedHelper _seleniumHelper;
        private readonly string _baseUrl;
        private readonly string _forumUrl;

        public BsuirBasedHelper(SeleniumBasedHelper seleniumHelper)
        {
            _seleniumHelper = seleniumHelper;
            _baseUrl = "http://www.bsuir.by/";
            _forumUrl = "http://www.bsuir.by/conference";
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
        }

        public string ForumUrl
        {
            get { return _forumUrl; }
        }

        public void GotoUrl(string url)
        {
            _seleniumHelper.GotoUrl(url);
        }



        public void SelectGroupSchedule(string group)
        {
            _seleniumHelper.FindElementByLinkText("Расписание групп").Click();
            var searchGroup = _seleniumHelper.FindElementById("studentGroupTab:studentGroupForm:searchStudentGroup");
            _seleniumHelper.ClearElement(searchGroup);
            _seleniumHelper.Type(searchGroup, "533701");

            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementById("studentGroupTab:studentGroupForm:j_idt21"));
        }

        public void MoveToShedules()
        {
            MoveToStaffLink();

            var schedulesLink = _seleniumHelper.FindElementByXpath(
                "//a[@href='/online/showpage.jsp?PageID=94599&resID=100229&lang=ru&menuItemID=101492']");
            _seleniumHelper.ClickByElement(schedulesLink);
        }

        private void MoveToStaffLink()
        {
            var menuHoverLink = _seleniumHelper.FindByCss("#main-menu > li:nth-child(1)");
            _seleniumHelper.MoveToElementAndClick(menuHoverLink);
        }


        public void MoveToContacts()
        {
            var contacts = _seleniumHelper.FindByCss("#m6 > a");
            _seleniumHelper.MoveToElementAndClick(contacts);
            var searchContact = _seleniumHelper.FindElementByXpath("//a[@href='/online/showpage.jsp?PageID=81055&resID=100229&lang=ru&menuItemID=121298']");
            _seleniumHelper.ClickByElement(searchContact);
        }

        public void SelectTeacherSchedule(string teacher)
        {
            _seleniumHelper.FindElementByLinkText("Расписание преподавателей").Click();
            var teacherInput = _seleniumHelper.FindElementById("employeeForm:searchEmployee_input");
            _seleniumHelper.ClearElement(teacherInput);
            _seleniumHelper.Type(teacherInput, teacher);

            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementById("employeeForm:j_idt21"));
        }

        public void FillContactForm()
        {
            var nameInput = _seleniumHelper.FindElementByName("newprop_14537_1");
            nameInput.Clear();
            _seleniumHelper.Type(nameInput, "Андрей");

            var contactInput = _seleniumHelper.FindElementByName("newprop_14538_1");
            contactInput.Clear();
            _seleniumHelper.Type(contactInput, "Контактная информация");

            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByName("save"));
        }

        public void ClearContactForm()
        {
            var nameInput = _seleniumHelper.FindElementByName("newprop_14537_1");
            nameInput.Clear();
            _seleniumHelper.Type(nameInput, "");

            var contactInput = _seleniumHelper.FindElementByName("newprop_14538_1");
            _seleniumHelper.ClearElement(contactInput);
            _seleniumHelper.Type(contactInput, "");

            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByXpath("//input[@value='Восстановить']"));

        }

        public void MoveToEapplications()
        {
            var menuHoverLink = _seleniumHelper.FindByCss("#m6 > a");
            _seleniumHelper.MoveToElementAndClick(menuHoverLink);

            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByLinkText("Электронное обращение"));
        }

        public void ElectronicCirculationFillForm()
        {
            var userInfo = _seleniumHelper.FindElementByName("newprop_21003_1");
            _seleniumHelper.ClearElement(userInfo);
            _seleniumHelper.Type(userInfo, "Краснов Андрей Юрьевич");

            var placeOfLivivng = _seleniumHelper.FindElementByName("newprop_21002_1");
            _seleniumHelper.ClearElement(placeOfLivivng);
            _seleniumHelper.Type(placeOfLivivng, "Ул.В.Хоружей");

            var emailInput = _seleniumHelper.FindElementByName("newprop_21001_1");
            _seleniumHelper.ClearElement(emailInput);
            _seleniumHelper.Type(emailInput, "krasnovandr@mail.ru");

            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByName("save"));
        }

        public void FacultySearch()
        {
            MoveToStaffLink();
            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByLinkText("Факультеты"));
            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByLinkText("Факультет компьютерных систем и сетей"));
            _seleniumHelper.ClickByElement(_seleniumHelper.FindByCss("div.icon_text > a"));
        }


        public void MoveToStudentLink()
        {
            var menuHoverLink = _seleniumHelper.FindByCss("#main-menu > li:nth-child(2)");
            _seleniumHelper.MoveToElementAndClick(menuHoverLink);
        }

        public void SearchEumkd(string teacher, string title)
        {
            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByLinkText("ЭРУД (ЭУМКД"));
            var teacherInput = _seleniumHelper.FindElementByName("obj_3_1");
            _seleniumHelper.ClearElement(teacherInput);
            _seleniumHelper.Type(teacherInput, teacher);

            var titleInput = _seleniumHelper.FindElementByName("obj_4_1");
            _seleniumHelper.ClearElement(titleInput);
            _seleniumHelper.Type(titleInput, title);

            _seleniumHelper.FindByCss("td.search_submit_td > input[type=\"submit\"]").Click();
        }


        public void LoginToForum(string login, string password)
        {
            _seleniumHelper.ClickByElement(_seleniumHelper.FindElementByLinkText("Вход"));
            var username = _seleniumHelper.FindElementById("username");
            _seleniumHelper.ClickByElement(username);
            _seleniumHelper.Type(username, login);

            var passwordInput = _seleniumHelper.FindElementById("password");
            _seleniumHelper.ClickByElement(passwordInput);
            _seleniumHelper.Type(passwordInput, password);

            _seleniumHelper.FindElementByName("login").Click();
        }
    }
}
