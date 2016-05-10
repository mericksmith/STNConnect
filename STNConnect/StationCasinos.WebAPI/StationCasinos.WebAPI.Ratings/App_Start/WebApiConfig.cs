using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace StationCasinos.WebAPI.Ratings
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ParimutuelApi",
                routeTemplate: "api/ratings/parimutuel",
                defaults: new { controller = "Parimutuel" }
            );

            config.Routes.MapHttpRoute(
                name: "InhouseApi",
                routeTemplate: "api/ratings/inhouse",
                defaults: new { controller = "Inhouse" }
            );
        }
    }
}
