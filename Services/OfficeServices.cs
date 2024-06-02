using Cargo.Contracts;
using Cargo.Data.Models;
using Cargo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Services
{
    public class OfficeServices : IOfficeServices
    {

        private IApplicationDbRepository repo;

        public OfficeServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddOffice([FromForm] Office model)
        {
            var existing = repo.All<Office>()
                .Where(r => r.Name == model.Name)
                .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("City already exist");
            }

            await repo.AddAsync(new Office()
            {
                Name = model.Name,
                CityId = model.CityId,

            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteOffice(int id)
        {
            await repo.DeleteAsync<Office>(id);

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditOffice([FromForm] Office model)
        {
            bool result = false;
            var office = await repo.GetByIdAsync<Office>(model.Id);

            if (office != null)
            {
                office.Name = model.Name;
                office.CityId = model.CityId;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public List<Office> GetAllOffices()
        {
            return repo.All<Office>().Include(x => x.City).ToList();
        }

        public Office PlaceholderOffice(int id)
        {
            return repo.All<Office>()
                .Where(x => x.Id == id)
                .Select(x => new Office()
                {
                    CityId = x.CityId,
                    Name = x.Name,
                })
                .First();
        }
    }
}
