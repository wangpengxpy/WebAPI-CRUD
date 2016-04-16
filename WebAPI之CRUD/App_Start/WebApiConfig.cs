using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebAPI之CRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var formatters = config.Formatters.Where(
                 formattter => formattter.SupportedMediaTypes.Where(
                     meta => meta.MediaType == "text/html" || meta.MediaType == "application/xml").Count() > 0
                     ).ToList();

            foreach (var formatter in formatters)
            {
                config.Formatters.Remove(formatter);
            }
        }
    }
}
