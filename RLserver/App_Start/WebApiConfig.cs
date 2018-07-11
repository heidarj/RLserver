using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace RLserver
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
        var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
        config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
  	
         var settings = config.Formatters.JsonFormatter.SerializerSettings;
  	
         settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
  	
         settings.Formatting = Formatting.Indented;

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
