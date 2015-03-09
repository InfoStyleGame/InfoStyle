using System.Web.Mvc;
using System.Web.Routing;
using infostyle.Configuration;

namespace infostyle
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            if (Config.NeedToStartFakeApi)
                routes.MapRoute(
                    "Fake api",
                    "Api/{apiControllerName}/{apiActionName}",
                    new { controller = "FakeApi", action = "Run" }
                    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Map", action = "Index"}
            );
        }
    }
}