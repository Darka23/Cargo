using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Contracts
{
    public interface IOrderServices
    {
        Task AddOrder([FromForm] Order order);
        List<Order> GetAllOrders();
        Task<bool> EditOrder([FromForm] Order order);
        Task DeleteOrder(int id);
        Order PlaceholderOrder(int id);
    }
}
 