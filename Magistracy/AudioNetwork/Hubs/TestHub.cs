using System;
using AudioNetwork.Services;
using DataLayer.Repositories;
using Microsoft.AspNet.SignalR;

namespace AudioNetwork.Hubs
{

    public class TestHub : Hub
    {
        //static List<TestUserModel> Users = new List<TestUserModel>();
  //      private readonly IUserService _userService;
        //private readonly IConversationRepository _conversationRepository;

        public TestHub()
        {
            
        }

        //public TestHub(IUserService userService)
        //{
        //    _userService = userService;
        //}

        public void TypingMessage()
        {
            //var user = _userService.GetUser(userId);
            Clients.All.newM();
        }

  


        //public void ParamCheck(string name,int age)
        //{
        //    Clients.AllExcept(Context.ConnectionId).acceptGreet(name+ age);
        //}

        //public void Send(string name, string message)
        //{
        //    Clients.All.addMessage(name, message);
        //}

        //// Подключение нового пользователя
        //public void Connect(string userName)
        //{
        //    var id = Context.ConnectionId;


        //    if (!Users.Any(x => x.ConnectionId == id))
        //    {
        //        Users.Add(new TestUserModel { ConnectionId = id, Name = userName });

        //        // Посылаем сообщение текущему пользователю
        //        Clients.Caller.onConnected(id, userName, Users);

        //        // Посылаем сообщение всем пользователям, кроме текущего
        //        Clients.AllExcept(id).onNewUserConnected(id, userName);
        //    }
        //}

        //// Отключение пользователя
        //public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        //{
        //    var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    if (item != null)
        //    {
        //        Users.Remove(item);
        //        var id = Context.ConnectionId;
        //        Clients.All.onUserDisconnected(id, item.Name);
        //    }

        //    return base.OnDisconnected(stopCalled);
        //}
    }
}