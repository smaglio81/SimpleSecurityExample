using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace SimpleSecurity.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			GlobalConfiguration.Configure(WebApiConfig.Register);

	        GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

	        // https://github.com/domaindrivendev/Swashbuckle/issues/113
	        GlobalConfiguration.Configuration
		        .Formatters
		        .JsonFormatter
		        .SerializerSettings
		        .ContractResolver = new CamelCasePropertyNamesContractResolver();

	        // http://stackoverflow.com/questions/9847564/how-do-i-get-asp-net-web-api-to-return-json-instead-of-xml-using-chrome
	        var jsonHeader = new RequestHeaderMapping(
		        "Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json");
	        GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(jsonHeader);

	        GlobalConfiguration.Configuration.EnsureInitialized();

	        SwaggerConfig.Register();
		}
    }
}
