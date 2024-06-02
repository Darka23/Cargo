using Cargo.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Areas.Ofis.Controllers
{
    public class OfisController : BaseController
    {
        private readonly IAutomobileServices automobileServices;
        private readonly IOrderServices orderServices;

        public OfisController(IAutomobileServices _automobileServices, IOrderServices _orderServices)
        {
            automobileServices = _automobileServices;
            orderServices = _orderServices;
        }
        public IActionResult AutomobileList(string? sortOrder)
        {
            var automobiles = automobileServices.GetAllAutomobiles().AsQueryable();

            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            ViewData["OfficeSortParam"] = sortOrder == "office" ? "office_desc" : "office";
            ViewData["KmSortParam"] = sortOrder == "km" ? "km_desc" : "km";

            automobiles = sortOrder switch
            {
                "desc" => automobiles.OrderByDescending(x => x.AutomobileModel.ModelName),
                "office" => automobiles.OrderBy(x => x.Office.Name),
                "office_desc" => automobiles.OrderByDescending(x => x.Office.Name),
                "km" => automobiles.OrderBy(x => x.Speedometer),
                "km_desc" => automobiles.OrderByDescending(x => x.Speedometer),
                _ => automobiles.OrderBy(x => x.AutomobileModel.ModelName)
            };

            return View(automobiles.ToList());
        }

        public IActionResult OrderList(string? sortOrder)
        {
            var orders = orderServices.GetAllOrders().AsQueryable();

            ViewData["ClientSortParam"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["CitySortParam"] = sortOrder == "city" ? "city_desc" : "city";
            ViewData["OfficeSortParam"] = sortOrder == "office" ? "office_desc" : "office";
            ViewData["AutomobileSortParam"] = sortOrder == "automobile" ? "automobile_desc" : "automobile";
            ViewData["TotalSortParam"] = sortOrder == "total" ? "total_desc" : "total";
            ViewData["KmSortParam"] = sortOrder == "km" ? "km_desc" : "km";

            orders = sortOrder switch
            {
                "desc" => orders.OrderByDescending(x => x.Client.FirstName),
                "date" => orders.OrderBy(x => x.OrderDate),
                "date_desc" => orders.OrderByDescending(x => x.OrderDate),
                "city" => orders.OrderBy(x => x.City.Name),
                "city_desc" => orders.OrderByDescending(x => x.City.Name),
                "office" => orders.OrderBy(x => x.Office.Name),
                "office_desc" => orders.OrderByDescending(x => x.Office.Name),
                "automobile" => orders.OrderBy(x => x.Automobile.AutomobileModel.ModelName),
                "automobile_desc" => orders.OrderByDescending(x => x.Automobile.AutomobileModel.ModelName),
                "total" => orders.OrderBy(x => x.TotalCost),
                "total_desc" => orders.OrderByDescending(x => x.TotalCost),
                "km" => orders.OrderBy(x => x.TravelledKilometers),
                "km_desc" => orders.OrderByDescending(x => x.TravelledKilometers),
                _ => orders.OrderBy(x => x.Client.FirstName)
            };

            return View(orders.ToList());
        }
    }
}
