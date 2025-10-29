using GymManagementDAL.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeeding
{
    public static class IDintityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            try
            {
             var HasUsers = userManager.Users.Any();
             var HasRoles = roleManager.Roles.Any();
                if (HasUsers && HasRoles)
                {
                    return false;
                }
                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                     new(){Name = "SuperAdmin"},
                     new(){Name = "Admin"}
                    };
                    foreach (var roles in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(roles.Name!).Result)
                        {
                            roleManager.CreateAsync(roles).Wait();
                        }
                    }
                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Youssef",
                        LastName = "Mohamed",
                        UserName = "YoussefMohamed",
                        Email = "yosefmohamed533255@gmail.com",
                        PhoneNumber = "01124594540"

                    };
                    userManager.CreateAsync(MainAdmin , "Youssef@2022").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Ahmed",
                        LastName = "Tourky",
                        UserName = "AhmedTourky",
                        Email = "ahmedTourky533255@gmail.com",
                        PhoneNumber = "01124594040"

                    };
                    userManager.CreateAsync(Admin, "Ahmed@2022").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "Admin").Wait();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed Faild : {ex}");
                return false;
            }
        }
    }
}
