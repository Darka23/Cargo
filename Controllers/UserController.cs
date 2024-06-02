using Cargo.Controllers;
using Cargo.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Offic"
            });

            return Ok();
        }
        public async Task<IActionResult> AddUserToAdministrator(string id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            await userManager.AddToRoleAsync(user, "Administrator");
            return Ok();

            //@User.FindFirstValue(ClaimTypes.NameIdentifier) => Current User Id
        }
    }
}
