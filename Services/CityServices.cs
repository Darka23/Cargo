using Cargo.Contracts;
using Cargo.Data.Models;
using Cargo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Services
{
    public class CityServices : ICityServices
    {

        private IApplicationDbRepository repo;

        public CityServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }


        public async Task AddCity([FromForm] City model)
        {
            var existing = repo.All<City>()
                .Where(r => r.Name == model.Name)
                .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("City already exist");
            }


            await repo.AddAsync(new City()
            {
                Name = model.Name,
            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteCity(int id)
        {
            await repo.DeleteAsync<City>(id);

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditCity([FromForm] City model)
        {
            bool result = false;
            var city = await repo.GetByIdAsync<City>(model.Id);

            if (city != null)
            {
                city.Name = model.Name;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public List<City> GetAllCities()
        {
            return repo.All<City>().ToList();
        }


    }
}
