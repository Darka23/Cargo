using Cargo.Contracts;
using Cargo.Data.Models;
using Cargo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Services
{
    public class AutomobileServices : IAutomobileServices
    {

        private IApplicationDbRepository repo;

        public AutomobileServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task AddAutomobile([FromForm] Automobile model)
        {
            var existing = repo.All<Automobile>()
               .Where(r => r.Id == model.Id)
               .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("Automobile already exist");
            }
            if (model.Speedometer<0)
            {
                throw new ArgumentException("Километражът не може да е 0");

            }
            await repo.AddAsync(new Automobile()
            {
                Speedometer = model.Speedometer,
                AutomobileModelId = model.AutomobileModelId,
                OfficeId = model.OfficeId,
            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteAutomobile(int id)
        {
            await repo.DeleteAsync<Automobile>(id);

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditAutomobile([FromForm] Automobile model)
        {
            bool result = false;
            var automobile = await repo.GetByIdAsync<Automobile>(model.Id);

            if (automobile != null)
            {
                automobile.Speedometer = model.Speedometer;
                automobile.AutomobileModelId = model.AutomobileModelId;
                automobile.OfficeId = model.OfficeId;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public List<Automobile> GetAllAutomobiles()
        {
            return repo.All<Automobile>()
                .Include(x => x.Office)
                .Include(x=>x.AutomobileModel)
                .ToList();
        }

        public Automobile PlaceholderAutomobile(int id)
        {
            return repo.All<Automobile>()
                .Where(x => x.Id == id)
                .Select(x => new Automobile()
                {
                    Speedometer = x.Speedometer,
                    OfficeId = x.OfficeId,
                    AutomobileModelId = x.AutomobileModelId
                })
                .First();
        }
    }
}
