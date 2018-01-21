using System;
using System.Net.Http;
using System.Web.Http;

namespace browsy.controller
{


    [RoutePrefix("browsy")]    
    public class resourceController:ApiController{
        [Route("")]
        [HttpGet]
        public HttpResponseMessage index() {
            string path=@"browsy.plugin.index.html";
            return core.EmbededResource(path,"html");
        }

        [Route("browsy.js")]
        [HttpGet]
        public HttpResponseMessage browsy_js() {
            string path=@"browsy.plugin.browsy.js";
            return core.EmbededResource(path,"js");
        }

        [Route("browsy.css")]
        [HttpGet]
        public HttpResponseMessage browsy_css() {
            string path=@"browsy.plugin.browsy.css";
            return core.EmbededResource(path,"css");
        }
    }
}
