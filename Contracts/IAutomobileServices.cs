using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Contracts
{
    public interface IAutomobileServices
    {
        Task AddAutomobile([FromForm] Automobile automobile);
        List<Automobile> GetAllAutomobiles();
        Task<bool> EditAutomobile([FromForm] Automobile automobile);
        Task DeleteAutomobile(int id);
        Automobile PlaceholderAutomobile(int id);
    }
}
