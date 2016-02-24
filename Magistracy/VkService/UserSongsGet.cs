using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkService.Models;

namespace VkService
{
    public class UserSongsGet
    {
        private readonly VkApi _vkApi;
        public UserSongsGet(string login, string password)
        {
            var vkAuthorization = new VkAuthorization(login, password);
            _vkApi = vkAuthorization.Authorize();
        }

     
    }
}
