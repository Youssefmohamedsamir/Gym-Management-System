using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.AccountViewModel;
using GymManagementDAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }


         #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            var User = _accountService.ValidateUser(model);

            if (User is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);
            }

            var Result = _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false).Result;

            if (Result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "Your Account Is Not Allowed");

            }
            if (Result.IsLockedOut)
            {
                ModelState.AddModelError("InvalidLogin", "Your Account Is Loged Out");

            }
            if (Result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        #endregion


        #region LogOut
        public ActionResult LogOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region AccessDenied

        public ActionResult AccessDenied()
        {
            return View();
        }


        #endregion


    }
    
}
