using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Business.Interfaces;
using Nidan.Extensions;

namespace Nidan.Attributes
{
    public class AuthorizePersonnelAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
                return false;    // The user is not authenticated

            var user = httpContext.User;
            if (!user.IsInRole("User"))
                return authorized;
            
            var routeData = httpContext.Request.RequestContext.RouteData;
            var id = routeData.Values["id"] as string;
            var personnelIdString = routeData.Values["personnelId"] as string;

            if (string.IsNullOrEmpty(id) && string.IsNullOrEmpty(personnelIdString))
                return false;   // No id was specified => we do not allow access
            
            var userManager = httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (userManager == null)
                return false;

            var applicationUser = userManager.FindById(user?.Identity?.GetUserId());
            if (applicationUser == null)
                return false;

            var userOrganisationId = applicationUser?.OrganisationId ?? 0;
            var userPersonnelId = applicationUser?.PersonnelId ?? 0;

            var personnelId = (personnelIdString ?? id).ToNullableInt();

            if (personnelId == userPersonnelId)
                return true;

            var hrBusinessService = DependencyResolver.Current.GetService<INidanBusinessService>();
            var permissions = hrBusinessService.RetrievePersonnelPermissions(user.IsInRole("Admin"), userOrganisationId, userPersonnelId, personnelId);

            return permissions.IsManager;
        }

    }
}