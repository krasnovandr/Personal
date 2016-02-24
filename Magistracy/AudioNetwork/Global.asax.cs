using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DataLayer.Models;
using Microsoft.AspNet.Identity;

namespace AudioNetwork
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Initialise();
        }


        protected void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            var id = HttpContext.Current.User.Identity.GetUserId();

            if (string.IsNullOrEmpty(id) == false)
            {
                using (var context = new ApplicationDbContext())
                {
                    try
                    {
                        var user = context.Users.Find(id);
                        user.LastActivity = DateTime.Now;
                        context.SaveChanges();
                    }
                    catch (Exception exp)
                    {
                        return;
                    }
                }
            }
        }
    }
}
