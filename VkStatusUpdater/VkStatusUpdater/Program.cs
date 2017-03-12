using System;
using System.IO;
using System.Linq;
using VkNet;

namespace VkStatusUpdater
{
    class Program
    {
        static void Main()
        {
            string login;
            string password;
            string fileName = "inf";

            if (File.Exists(fileName))
            {
                var results = File.ReadAllLines(fileName);
                login = results.First();
                password = results[1];
            }
            else
            {
                do
                {
                    Console.WriteLine("Логин");
                    login = Console.ReadLine();

                    Console.WriteLine("Пароль");
                    password = Console.ReadLine();
                } while (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password));
            }




            var authorize = new VkAuthorization(login, password);
            //var authorize = new VkAuthorization();
            VkApi api = null;
            try
            {
                api = authorize.Authorize();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Что то не так с авторизацией");
            }

            if (api == null)
            {
                Console.WriteLine("Что то не так с авторизацией");
                Console.WriteLine("Нажмите любую клавишу для завершения");
                Console.ReadKey();
                return;
            }
           
            File.WriteAllLines(fileName, new string[] { login, password });
            var meetDateTime = new DateTime(2013, 9, 18, 18, 0, 0);

            string currentStatus = string.Empty;
            try
            {
                if (api.UserId != null)
                {
                    currentStatus = api.Status.Get((long)api.UserId).Text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не найден пользователь");
            }

            var heartIndex =currentStatus.IndexOf(" ");
            if (heartIndex != -1)
            {
                currentStatus = currentStatus.Substring(0, heartIndex); 
            }
 
            var result = (DateTime.Now - meetDateTime).Days;
            Console.WriteLine("Предыдущий статус {0}", currentStatus);

            int currentStatusValue;
            int.TryParse(currentStatus, out currentStatusValue);
            if (currentStatusValue != result)
            {
                Console.WriteLine("Новый статус {0}", result);
                api.Status.Set(result.ToString() + " " + "<3");
            }
            else
            {
                Console.WriteLine("Cтатусы равны");
            }

            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
    }
}
