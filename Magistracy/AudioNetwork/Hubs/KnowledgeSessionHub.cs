using Microsoft.AspNet.SignalR;

namespace AudioNetwork.Web.Hubs
{
    public class KnowledgeSessionHub : Hub
    {
        public void SendMessage(int nodeId)
        {
            Clients.All.newMessage(nodeId);
        }
    }
}