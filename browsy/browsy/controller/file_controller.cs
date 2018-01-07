using System;
using System.Web.Http;
using System.Net.Http;

namespace browsy.controller
{

        
    [RoutePrefix("server")]    
    public class serverController:ApiController{

        public serverController(){

        }

        [Route("heartbeat")]
        [HttpPost]
        public string heartbeat() {
            return DateTime.UtcNow.ToString();
        }

        [Route("get_dir")]
        [HttpPost]
        public browsy.model.directory get_dir(browsy.model.dir_path path) {
        browsy.model.directory d=new browsy.model.directory();

            try{
                d.get(path);
            } catch {
                return d;
            }
            return d;
        }

    }
}
