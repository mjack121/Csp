﻿using Csp.Web.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Csp.Web.Extensions
{
    public static class RouteBuilderExtensions
    {
        public static IRouteBuilder MapDomainRoute(this IRouteBuilder routeBuilder, string domain, string area)
        {
            if (string.IsNullOrEmpty(area) )
            {
                throw new ArgumentNullException("area or controller can not be null");
            }
            var inlineConstraintResolver = routeBuilder
                .ServiceProvider
                .GetRequiredService<IInlineConstraintResolver>();

            string template = "";

            RouteValueDictionary defaults = new RouteValueDictionary
            {
                { "area", area },
                { "controller",  "home" },
                { "action",  "index" }
            };
            RouteValueDictionary constrains = new RouteValueDictionary
            {
                { "area", area },
                { "controller",  "home" }
            };
            //constrains.Add("controller", controller);
            //defaults.Add("controller", string.IsNullOrEmpty(controller) ? "home" : controller);
            //defaults.Add("action", "index");

            template += "{action}/{id?}";//路径规则中不再包含控制器信息，但是上面通过constrains限定了查找时所要求的控制器名称
            routeBuilder.Routes.Add(new SubDomainRouter(routeBuilder.DefaultHandler, template, domain, inlineConstraintResolver, defaults, constrains,null));


            return routeBuilder;
        }

    }
}
