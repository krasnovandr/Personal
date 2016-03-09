using AudioNetwork.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace AudioNetwork.Web
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
            //var hubConfiguration = new HubConfiguration
            //{
            //    EnableDetailedErrors = true,
            //    EnableJavaScriptProxies = false
            //};
            //app.MapSignalR(hubConfiguration);
            app.MapSignalR();
        }
    }
}
