using GymManagementBLL.Services.Interface;
using GymManagementBLL.ViewModel.AccountViewModel;
using GymManagementDAL.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Sevice
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> userManager)
        { 
        _userManager = userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
           var User = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
            if (User == null) return null;

            var IsPasswordValid = _userManager.CheckPasswordAsync(User , loginViewModel.Password).Result;
            return IsPasswordValid ? User : null;
        }
    }
}
