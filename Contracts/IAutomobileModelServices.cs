using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Contracts
{
    public interface IAutomobileModelServices
    {
        Task AddAutomobileModel([FromForm] AutomobileModel automobileModel);
        List<AutomobileModel> GetAllAutomobileModels();
        Task<bool> EditAutomobileModel([FromForm] AutomobileModel automobileModel);
        Task DeleteAutomobileModel(int id);
        AutomobileModel PlaceholderAutomobileModel(int id);
    }
}
