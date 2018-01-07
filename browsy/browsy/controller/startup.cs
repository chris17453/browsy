using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Cors;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;

namespace browsy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app){
            var webApiConfiguration = ConfigureWebApi();
            //app.UseWelcomePage();
            app.UseErrorPage();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            const string rootFolder = "../../";
            var fileSystem=new PhysicalFileSystem(rootFolder);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };

            app.UseFileServer(options);

            app.UseWebApi(webApiConfiguration);
        }

        private HttpConfiguration ConfigureWebApi(){
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi","api/{controller}/{id}",new { id = RouteParameter.Optional });
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
