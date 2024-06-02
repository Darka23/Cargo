using Cargo.Data.Identity;
using Cargo.ViewModels;

namespace Cargo.Contracts
{
    public interface IUserServices
    {
        List<ApplicationUser> GetAllUsers();
        Task SetUserOffice(string id);
        void DeleteUser(string id);
        Task<bool> EditUser(ApplicationUser model);
        UserEditViewModel? PlaceholderUser(string id);

    }
}
