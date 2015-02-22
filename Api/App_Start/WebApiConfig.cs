using System.IO;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using log4net.Config;

namespace Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "Hello", id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            var l4NetPath = HostingEnvironment.MapPath("~/bin/log4net.config");
            XmlConfigurator.ConfigureAndWatch(new FileInfo(l4NetPath));
        }
    }
}
