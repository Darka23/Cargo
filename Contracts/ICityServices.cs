using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Contracts
{
    public interface ICityServices
    {
        Task AddCity([FromForm] City city);
        List<City> GetAllCities();
        Task<bool> EditCity([FromForm] City city);
        Task DeleteCity(int id);
    }
}
