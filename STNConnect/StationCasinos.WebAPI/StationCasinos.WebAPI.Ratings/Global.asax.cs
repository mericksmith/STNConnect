using StationCasinos.WebAPI.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;
using StationCasinos.WebAPI.Utility.Interface;

namespace StationCasinos.WebAPI.Ratings
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
