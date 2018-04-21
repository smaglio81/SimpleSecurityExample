using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using Elmah.Contrib.WebApi;
using Microsoft.Web.Http.Description;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Ucsb.Sa.Enterprise.AspNet.WebApi;
using Ucsb.Sa.Enterprise.MvcExtensions.Versioning;

namespace SimpleSecurity.Web
{
    public static class WebApiConfig
    {
	    public static VersionedApiExplorer ApiExplorer { get; set; }

		public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services

			//	Versioning configuration
			var constraintResolver = new DefaultInlineConstraintResolver()
			{
				ConstraintMap = { ["apiVersion"] = typeof(UcsbApiVersionWithOverrideRouteConstraint) }
			};
			config.MapHttpAttributeRoutes(constraintResolver);

			// this is needed for swagger to work
			ApiExplorer = config.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

			// calculates the highest implemented version for later use
			var currentAssembly = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Assembly;
			var versionAggregator = new UcsbApiVersionAggregator(new[] { currentAssembly });

			UcsbApiVersionWithOverrideRouteConstraint.DefaultVersionAggregator = versionAggregator;

			config.AddApiVersioning(o =>
			{
				// reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
				o.ReportApiVersions = true;

				// this sets up the value so the system can use it elsewhere
				o.DefaultApiVersion = versionAggregator.HighestImplementedVersion;

				// if the {apiVersion} isn't in the url path (ie. an unversioned path), then the read is
				// used first to try and determine the version number (like, from the "ucsb-api-version" header).
				// if it fails to find a path then the Selector (below) might be used.
				o.ApiVersionReader = new UcsbApiVersionReader();


				o.AssumeDefaultVersionWhenUnspecified = true;   // default is false (kinda annoying)
																// in order for the Selector's 'Select' function to be called two things have to be true:
																//	1) the AssumeDefaultVersionWhenUnspecified (above) has to be 'true'
																//	2) the {apiVersion} can't be in the url path (ie. an unversioned path)
				o.ApiVersionSelector = new UcsbCurrentImplementationWithOverrideApiVersionSelector(o, versionAggregator);
			});

			//	adds "api-selected-version" response header
			config.Filters.Add(new UcsbSelectedApiVersionAttribute());

			// Web API routes
			//config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi-Versioned",
				routeTemplate: "v{apiVersion}/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional },
				constraints: new { apiVersion = new UcsbApiVersionWithOverrideRouteConstraint(versionAggregator) }
			);

			GlobalConfiguration.Configuration.Filters.Add(new TraceApiCallAttribute() { RecordResponseBody = false });
	        GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());
			
        }
    }
}
