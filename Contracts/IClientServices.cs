using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Contracts
{
    public interface IClientServices
    {
        Task AddClient([FromForm] Client client);
        List<Client> GetAllClients();
        Task<bool> EditClient([FromForm] Client client);
        Task DeleteClient(int id);
        Client PlaceholderClient(int id);
    }
}
