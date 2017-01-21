using System.Web.Mvc;
using System.Web.Routing;
using Nidan.Constraints;
using Nidan.Interfaces;

namespace Nidan.Extensions
{
    public static class RouteExtensions
    {
        public static void MapRouteWithTenantConstraint(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRoute(
                name,
                url,
                defaults,
                new { TenantAccess = new TenantRouteConstraint(DependencyResolver.Current.GetService<ITenantsService>()) }
            );
        }
    }
}