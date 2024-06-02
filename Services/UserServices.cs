using Cargo.Contracts;
using Cargo.Data.Identity;
using Cargo.Data.Repositories;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Cargo.Services
{
    public class UserServices : IUserServices
    {
        private IApplicationDbRepository repo;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UserServices(IApplicationDbRepository _repo,
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            repo = _repo;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return repo.All<ApplicationUser>().ToList();
        }

        public async Task SetUserOffice(string id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            await userManager.AddToRoleAsync(user, "Office");

            var repoUser = await repo.GetByIdAsync<ApplicationUser>(id.ToString());
            repoUser.Role = "Office";

            await repo.SaveChangesAsync();
        }

        public void DeleteUser(string id)
        {
            var user = repo.All<ApplicationUser>()
                .Where(x => x.Id == id)
                .First();

            if (user != null)
            {
                repo.Delete<ApplicationUser>(user);
                repo.SaveChanges();
            }
        }
        public async Task<bool> EditUser(ApplicationUser model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if (user != null)
            {
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public UserEditViewModel? PlaceholderUser(string id)
        {
            return repo.All<ApplicationUser>()
                .Where(r => r.Id == id)
                .Select(r => new UserEditViewModel()
                {
                    Id = id,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    UserName = r.UserName
                })
                .FirstOrDefault();
        }
    }
}
