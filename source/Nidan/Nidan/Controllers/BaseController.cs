using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Nidan.Business.Interfaces;
using Nidan.Entity.Dto;
using Nidan.Models;
using Nidan.Models.Authorization;

namespace Nidan.Controllers
{
    public class BaseController : Controller
    {
        private INidanBusinessService _nidanBusinessService;
        private ApplicationUserManager _userManager;
        private ApplicationUser _applicationUser;

        protected INidanBusinessService NidanBusinessService
        {
            get
            {
                return _nidanBusinessService;
            }
        }

        public BaseController(INidanBusinessService nidanBusinessService)
        {
            _nidanBusinessService = nidanBusinessService;
        }



        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected ApplicationUser ApplicationUser
        {
            get
            {
                return _applicationUser ?? UserManager.FindById(User?.Identity?.GetUserId());
            }
            set
            {
                _applicationUser = value;
            }
        }

        protected TenantOrganisation Organisation => UserManager.TenantOrganisation;


        protected int UserOrganisationId => ApplicationUser?.OrganisationId ?? 0;
        protected int UserPersonnelId => ApplicationUser?.PersonnelId ?? 0;
        protected int UserCentreId => ApplicationUser?.CentreId ?? 0;
        // protected int UserEnquiryId => ApplicationUser?.EnquiryId?? 0;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewModel = filterContext.Controller.ViewData.Model as BaseViewModel;

            if (viewModel != null)
            {
                var organisation = UserManager.TenantOrganisation;
                viewModel.OrganisationName = organisation?.Name ?? string.Empty;
                viewModel.CentreName = NidanBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId, e => true)?.Name ?? viewModel.OrganisationName;
                viewModel.PersonnelId = UserPersonnelId;
                viewModel.CentreId = UserCentreId;
                // viewModel.EnquiryId = UserEnquiryId;

            }

            base.OnActionExecuted(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_nidanBusinessService != null)
                    _nidanBusinessService = null;

                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_applicationUser != null)
                    _applicationUser = null;

            }

            base.Dispose(disposing);
        }
    }
}