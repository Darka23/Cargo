using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Contracts
{
    public interface IOfficeServices
    {
        Task AddOffice([FromForm] Office office);
        List<Office> GetAllOffices();
        Task<bool> EditOffice([FromForm] Office office);
        Task DeleteOffice(int id);
        Office PlaceholderOffice(int id);
    }
}
