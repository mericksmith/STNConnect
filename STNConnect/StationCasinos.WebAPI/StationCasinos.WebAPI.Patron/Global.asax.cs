using StationCasinos.WebAPI.Utility.Interface;
using System.Web.Http;


namespace StationCasinos.WebAPI.Patron
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
        }

        protected void Application_BeginRequest()
        {
            ILogging logger = (ILogging)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogging));
            logger.SetReuqestId(System.Guid.NewGuid().ToString());
        }
    }
}
