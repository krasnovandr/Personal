using AudioNetwork.Hubs;
using AudioNetwork.Services;
using DataLayer.Repositories;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AudioNetwork.Startup))]
namespace AudioNetwork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

          //  GlobalHost.DependencyResolver.Register(
          //typeof(ConversationHub),
          //() => new ConversationHub(new UserService(new UserRepository(), new MusicRepository())));
        
          //  GlobalHost.DependencyResolver.Register(
          //typeof(TestHub),
          //() => new TestHub(new UserService(new UserRepository(), new MusicRepository())));
            
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
