using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace joestrommen.com
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "EmailSignup",
				url: "email-signup",
				defaults: new { controller = "Email", action = "Signup", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Encyclopedia",
				url: "web-performance-encyclopedia/{id}",
				defaults: new { controller = "WebPerformanceEncyclopedia", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Blog",
				url: "Blog/{id}",
				defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "HomeRoutes",
				url: "{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
