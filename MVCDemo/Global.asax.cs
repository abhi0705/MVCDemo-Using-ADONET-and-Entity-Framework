using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using MVCDemo.Filters;
using System.Web.Http;
using System.Web.Routing;

namespace MVCDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<MVCDemo.Models.RegistrationContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
 