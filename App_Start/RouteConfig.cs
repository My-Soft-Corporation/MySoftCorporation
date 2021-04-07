using System.Web.Mvc;
using System.Web.Routing;

namespace MySoftCorporation
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
          name: "MainCourses",
          url: "Course/{action}/{id}",
          defaults: new { area = "", controller = "Course", action = "Index", id = UrlParameter.Optional },
          namespaces: new[] { "MySoftCorporation.Controllers" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}