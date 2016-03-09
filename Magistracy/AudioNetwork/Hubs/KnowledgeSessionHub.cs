using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ServiceLayer.Models;

namespace AudioNetwork.Web.Hubs
{
    public class KnowledgeSessionHub : Hub
    {
        private List<UserViewModel> _users;


        public void FirstRoundStarted()
        {
            Clients.All.updateClient("lol");
        }
    }
}