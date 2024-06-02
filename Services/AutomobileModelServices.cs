using Cargo.Contracts;
using Cargo.Data.Models;
using Cargo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Services
{
    public class AutomobileModelServices : IAutomobileModelServices
    {
        private IApplicationDbRepository repo;

        public AutomobileModelServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddAutomobileModel([FromForm] AutomobileModel model)
        {
            var existing = repo.All<AutomobileModel>()
                .Where(r => r.ModelName == model.ModelName)
                .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("AutomobileModel already exist");
            }

            if (model.FuelConsumption < 0 || model.MaxLoad < 0)
            {
                throw new ArgumentException("Разходът и товарът не могат да бъдат отрицателни числа");
            }

            await repo.AddAsync(new AutomobileModel()
            {
               ModelName = model.ModelName,
               FuelConsumption = model.FuelConsumption,
               Manufacturer = model.Manufacturer,
               MaxLoad = model.MaxLoad,
               ReleaseYear = model.ReleaseYear,
            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteAutomobileModel(int id)
        {
            await repo.DeleteAsync<AutomobileModel>(id);

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditAutomobileModel([FromForm] AutomobileModel model)
        {
            bool result = false;
            var autoModel = await repo.GetByIdAsync<AutomobileModel>(model.Id);
            if (model.FuelConsumption < 0 || model.MaxLoad < 0)
            {
                throw new ArgumentException("Разходът и товарът не могат да бъдат отрицателни числа");
            }
            if (autoModel != null)
            {
                autoModel.ModelName = model.ModelName;
                autoModel.FuelConsumption = model.FuelConsumption;
                autoModel.Manufacturer = model.Manufacturer;
                autoModel.MaxLoad = model.MaxLoad;
                autoModel.ReleaseYear = model.ReleaseYear;
                

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public List<AutomobileModel> GetAllAutomobileModels()
        {
            return repo.All<AutomobileModel>().ToList();
        }

        public AutomobileModel PlaceholderAutomobileModel(int id)
        {
            return repo.All<AutomobileModel>()
                .Where(x => x.Id == id)
                .Select(x => new AutomobileModel()
                {
                    ModelName = x.ModelName,
                    FuelConsumption = x.FuelConsumption,
                    Manufacturer = x.Manufacturer,
                    MaxLoad = x.MaxLoad,
                    ReleaseYear = x.ReleaseYear,
                })
                .First();
        }
    }
}
