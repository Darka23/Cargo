using Cargo.Contracts;
using Cargo.Data.Models;
using Cargo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Services
{
    public class OrderServices : IOrderServices
    {
        private IApplicationDbRepository repo;

        public OrderServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddOrder([FromForm] Order order)
        {
            if (order.TotalCost < 0 || order.TravelledKilometers <0)
            {
                throw new ArgumentException("Цената и километрите не могат да са отциателни");

            }
            await repo.AddAsync(new Order()
            {
                AutomobileId = order.AutomobileId,
                CityId = order.CityId,
                ClientId = order.ClientId,
                OfficeId = order.OfficeId,
                OrderDate = order.OrderDate,
                TotalCost = order.TotalCost,
                TravelledKilometers = order.TravelledKilometers,
            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            await repo.DeleteAsync<Order>(id);

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditOrder([FromForm] Order model)
        {
            bool result = false;
            var order = await repo.GetByIdAsync<Order>(model.Id);

            if (order != null)
            {
                order.TravelledKilometers = model.TravelledKilometers;
                order.TotalCost = model.TotalCost;
                order.AutomobileId = model.AutomobileId;
                order.CityId = model.CityId;
                order.OfficeId = model.OfficeId;
                order.OrderDate = model.OrderDate;
                order.ClientId = model.ClientId;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public List<Order> GetAllOrders()
        {
            return repo.All<Order>()
                .Include(x => x.City)
                .Include(x => x.Office)
                .Include(x=>x.Automobile)
                .ThenInclude(x=>x.AutomobileModel)
                .Include(x=>x.Client)
                .ToList();
        }

        public Order PlaceholderOrder(int id)
        {
            return repo.All<Order>()
                .Where(x => x.Id == id)
                .Select(x => new Order()
                {
                    ClientId = x.ClientId,
                    AutomobileId = x.AutomobileId,
                    OfficeId = x.OfficeId,
                    OrderDate = x.OrderDate,
                    TravelledKilometers = x.TravelledKilometers,
                    CityId = x.CityId,
                    TotalCost = x.TotalCost
                })
                .First();
        }
    }
}
