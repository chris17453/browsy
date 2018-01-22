# browsy
Browsy Is a client side file browser for your server. 


## System Software Components 
- a webAPI
- a jQuery Plugin


## Deployment
The dll includes everything needed to run browsy. The webAPI Controller, and web resources.

The web resources are:
- /browsy/index.html
- /browsy/browsy.js
- /browsy/browsy.css

The project namespace is "browsy"

To load the webAPI Controller:
- reference browsy.dll 
- Add the namespace to your webAPI route


### C# WebAPI Backend Example:
```c#
var r=config.Routes.MapHttpRoute(name          :"DefaultApi",
                                 routeTemplate :"api/{controller}/{id}",
                                 defaults      :new { id = RouteParameter.Optional });

//this is where you add the linked controllers. everything is inside of the DLL
r.DataTokens["Namespaces"] = new[] { typeof(browsy.controller.serverController).Namespace };
```
### Javascript Client Side Example
```javascript
$(filter).browsy({
                path                : null,                              //default path for the browser
                filters             : [],                                //An array of filters for files. [".png",".jpg"]
                admin               : true,                              //turn on all admin controls
                server              : "http://localhost:8888",           //The api URL
                admin_hidden_folders: false,                             //turn on the hidden folder toggle
                admin_hidden_files  : false,                             //turn on the hidden file toggle
                admin_filters       : false,                             //turn on the filter toggle
                controller_name     : "browsy",                          //the api controller name (if you change it)
                special_dirs        : [{ name:"Desktop",                 //use this to replace all special icons and sticky folders
                                                icon:"fas fa-desktop",
                                                type:"folder" ,
                                                ext:"",
                                                special:true
                                      }],
                success_function    : null,                              //success callback
                cancel_function     : null,                              //cancel api callback
                dialog_function     : null,                              //custom call back for messages
                api_error           : "Bummer... Error loading the file browser data.",  //system error message
                no_file_error       : "Gotta pick a file first captain."                 //no selected file mesage
}); 

```
    

## External Requirements:
- fontawesome5
- bootstrap4
- bootstrap-toggle


## Contributing
Please read the COINTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.


## Versioning
We use GIT for versioning. For versions available, see the tags on this repository.


## License
This project is licensed under the GNU General Public License v3 -- see the LICENSE.md file for details


## Authors
Charles Watkins


## Contact Info
- (chris17453@gmail.com)
- [https://github.com/chris17453/browsy/](https://github.com/chris17453/browsy/)
- [https://www.linkedin.com/in/chris17453/](https://www.linkedin.com/in/chris17453/)
