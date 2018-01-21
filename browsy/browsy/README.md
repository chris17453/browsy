# browsy
Browsy Is a client side file browser for your server. 


## System Software Components 
a webAPI
a jQuery Plugin


## Deployment
The dll includes everything needed to run browsy. The webAPI Controller, and web resources.

The web resources are:
/browsy/index.html	
/browsy/browsy.js
/browsy/browsy.css

The project namespace is "browsy"

To load the webAPI Controller:
 reference browsy.dll 
 Add the namespace to your webAPI route

### Example:
```c#
var r=config.Routes.MapHttpRoute(name          :"DefaultApi",
				 routeTemplate :"api/{controller}/{id}",
				 defaults      :new { id = RouteParameter.Optional });

//this is where you add the linked controllers. everything is inside of the DLL
r.DataTokens["Namespaces"] = new[] { typeof(browsy.controller.serverController).Namespace };
```

## External Requirements:
fontawesome5
bootstrap4
bootstrap-toggle


## Contributing
Please read the COINTRIBUTING.md for details on our code of conduct, and the process for submitting pull requests to us.


## Versioning
We use GIT for versioning. For versions available, see the tags on this repository.


## License
This project is licensed under the GNU General Public License v3 -- see the LICENSE.md file for details



## Authors
Charles Watkins

##Contact Info
chris17453@gmail.com
https://github.com/chris17453/browsy/
https://www.linkedin.com/in/chris17453/
