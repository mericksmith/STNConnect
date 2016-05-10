using StationCasinos.WebAPI.Resolver;
using System.Web.Http;

namespace StationCasinos.WebAPI.Patron
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver();
        }
    }
}