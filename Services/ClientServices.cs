using Cargo.Contracts;
using Cargo.Data.Models;
using Cargo.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Services
{
    public class ClientServices : IClientServices
    {
        private IApplicationDbRepository repo;

        public ClientServices(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddClient([FromForm] Client model)
        {
            var existing = repo.All<Client>()
                .Where(r => r.Email == model.Email)
                .FirstOrDefault();

            if (existing != null)
            {
                throw new ArgumentException("Client already exist");
            }

            if (model.DateOfBirth > DateTime.Now)
            {
                throw new ArgumentException("DateOfBirth cannot be after now");
            }

            if (model.CreditCardExpirationDate < DateTime.Now)
            {
                throw new ArgumentException("Credit card has expired");
            }

            await repo.AddAsync(new Client()
            {
                Email = model.Email,
                CreditCardExpirationDate = model.CreditCardExpirationDate,
                CreditCardNumber = model.CreditCardNumber,
                DateOfBirth = model.DateOfBirth,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
            });

            await repo.SaveChangesAsync();
        }

        public async Task DeleteClient(int id)
        {
            await repo.DeleteAsync<Client>(id);

            await repo.SaveChangesAsync();
        }

        public async Task<bool> EditClient([FromForm] Client model)
        {
            bool result = false;
            var client = await repo.GetByIdAsync<Client>(model.Id);

            if (model.DateOfBirth > DateTime.Now)
            {
                throw new ArgumentException("DateOfBirth cannot be after now");
            }

            if (model.CreditCardExpirationDate <= DateTime.Now)
            {
                throw new ArgumentException("Credit card has expired");
            }

            if (client != null)
            {
                client.CreditCardNumber = model.CreditCardNumber;
                client.FirstName = model.FirstName;
                client.LastName = model.LastName;
                client.Gender = model.Gender;
                client.DateOfBirth = model.DateOfBirth;
                client.FirstName = model.FirstName;
                client.Email = model.Email;
                client.CreditCardExpirationDate = model.CreditCardExpirationDate;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public List<Client> GetAllClients()
        {
            return repo.All<Client>().ToList();
        }

        public Client PlaceholderClient(int id)
        {

            return repo.All<Client>()
                .Where(x => x.Id == id)
                .Select(x => new Client()
                {
                    CreditCardExpirationDate = x.CreditCardExpirationDate,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Gender = x.Gender,
                    DateOfBirth = x.DateOfBirth,
                    CreditCardNumber = x.CreditCardNumber,
                    Email = x.Email,
                })
                .First();
        }
    }
}
