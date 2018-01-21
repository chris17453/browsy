using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Cors;

namespace browsy_server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app){
            var webApiConfiguration = ConfigureWebApi();
            app.UseErrorPage();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(webApiConfiguration);
        }

        private HttpConfiguration ConfigureWebApi(){
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var r=config.Routes.MapHttpRoute(
                                        name               :"DefaultApi",
                                        routeTemplate      :  "api/{controller}/{id}",
                                        defaults           :new { id = RouteParameter.Optional });

            //this is where you add the linked controllers. everything is inside of the DLL
            r.DataTokens["Namespaces"] = new[] { typeof(browsy.controller.serverController).Namespace };

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings =
                      new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return config;
        }
    }

}
