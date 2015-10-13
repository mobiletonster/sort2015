using Newtonsoft.Json.Serialization;
using Sort2015.Data.Models;
using Sort2015.Web.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace Sort2015.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Add(new CSVMediaTypeFormatter());

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<DailyGem>("DailyGems");
            builder.EntitySet<Author>("Authors");

            config.Routes.MapODataRoute("ODataRoute", "odata", builder.GetEdmModel());
        }
    }
}
