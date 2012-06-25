using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Routing;
using CarManager.Data;
using CarManager.Web.Controllers;


namespace CarManager.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class WebApiApplication : System.Web.HttpApplication
	{
		

		public static void RegisterRoutes(RouteCollection routes)
		{

			routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/car/{id}",
				defaults: new { id = RouteParameter.Optional, controller = "Car" }
				);

			routes.MapHttpRoute(
				name: "DefaultApiWithAction",
				routeTemplate: "api/cars/{action}",
				defaults: new { action = "", controller = "Cars" }

			);

		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
		}
	}
}