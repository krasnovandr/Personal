using System;
using VkNet;
using VkNet.Enums.Filters;

namespace VkService
{
    public class VkAuthorization
    {
        private const int AppId = 4806454;
        string _login;
        string _pass;
        private readonly Settings _scope = Settings.All;

        public VkAuthorization()
        {
            _login = "375293323876";
            _pass = "3323876may1993";
        }

        public VkAuthorization(string login, string pass)
        {
            this._login = login;
            this._pass = pass;
        }

        public VkApi Authorize()
        {
            var vk = new VkApi();
            if (string.IsNullOrEmpty(_login) == false && string.IsNullOrEmpty(_pass) == false)
            {
                try
                {
                    vk.Authorize(AppId, _login, _pass, _scope);
                }
                catch (Exception ex)
                {
                    return null;
                }


                return vk;
            }

            return null;
        }
    }
}
