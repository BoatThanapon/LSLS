using System.Web.Mvc;
using System.Web.Security;
using LSLS.Repository.Abstract;
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
        public ViewResult ViewLoginStaff()
        {
            return View(Request.IsAuthenticated ? "Main" : "ViewLoginStaff");
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("ViewLoginStaff")]
        public ActionResult CheckLoginStaff(LoginStaffViewModel loginStaff)
        {
            var staff = _authProvider.AuthenticateStaff(loginStaff);

            if (staff != null)
                return RedirectToAction("Main", "Account");
            ModelState.AddModelError("", "");

            return View("ViewLoginStaff", loginStaff);
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
            return RedirectToAction("ViewLoginStaff", "Account");
        }
    }

}