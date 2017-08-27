using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LSLS.Models;
using LSLS.Repository;
using LSLS.ViewModels;

namespace LSLS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }


        // GET: Account    
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost, ActionName("Login")]
        public ActionResult CheckLogin(LoginStaffViewModel loginStaff)
        {
            Staff staff = _authProvider.AuthenticateStaff(loginStaff);

            if (staff != null)
            {
                return RedirectToAction("Main", "Account");                
            }
            ModelState.AddModelError("", "");

            return View(loginStaff);
        
            
        }

        [Authorize]
        public ViewResult Main()
        {
            return View("Main");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}