using Cargo.Contracts;
using Cargo.Data.Identity;
using Cargo.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cargo.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ICityServices cityServices;
        private readonly IClientServices clientServices;
        private readonly IOfficeServices officeServices;
        private readonly IAutomobileModelServices automobileModelServices;
        private readonly IAutomobileServices automobileServices;
        private readonly IOrderServices orderServices;
        private readonly IUserServices userServices;

        public AdminController(
            ICityServices _cityServices,
            IClientServices _clientServices,
            IOfficeServices _officeServices,
            IAutomobileModelServices _automobileModelServices,
            IAutomobileServices _automobileServices,
            IOrderServices _orderServices,
            IUserServices _userServices)
        {
            cityServices = _cityServices;
            clientServices = _clientServices;
            officeServices = _officeServices;
            automobileModelServices = _automobileModelServices;
            automobileServices = _automobileServices;
            orderServices = _orderServices;
            userServices = _userServices;
        }

        // USER //

        public IActionResult AdminPanel()
        {
            return View();
        }

        public IActionResult UserList()
        {
            var model = new
            {
                Users = userServices.GetAllUsers()
            };

            return View(model);
        }

        public IActionResult DeleteUser(string id)
        {
            userServices.DeleteUser(id);

            return RedirectToAction("UserList", "Admin");
        }
        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var currUser = userServices.PlaceholderUser(id);
            return View(currUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model)
        {
            if (await userServices.EditUser(model))
            {
                return RedirectToAction("UserList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> SetUserAsOffice(string id)
        {
            await userServices.SetUserOffice(id);

            return RedirectToAction("UserList", "Admin");
        }

        // USER //


        // AUTOMOBILE //
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

        [HttpGet]
        public IActionResult AddAutomobile()
        {
            ViewData["AutomobileModelId"] = new SelectList(automobileModelServices.GetAllAutomobileModels(), "Id", "ModelName");
            ViewData["OfficeId"] = new SelectList(officeServices.GetAllOffices(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAutomobile([FromForm] Automobile automobile)
        {
            await automobileServices.AddAutomobile(automobile);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditAutomobile(int id)
        {
            var placeholder = automobileServices.PlaceholderAutomobile(id);
            ViewData["AutomobileModelId"] = new SelectList(automobileModelServices.GetAllAutomobileModels(), "Id", "ModelName");
            ViewData["OfficeId"] = new SelectList(officeServices.GetAllOffices(), "Id", "Name");
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditAutomobile([FromForm] Automobile automobile)
        {
            if (await automobileServices.EditAutomobile(automobile))
            {
                return RedirectToAction("AutomobileList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteAutomobile(int id)
        {
            await automobileServices.DeleteAutomobile(id);

            return RedirectToAction("AutomobileList", "Admin");
        }
        // AUTOMOBILE //


        // AUTOMOBILE MODEL //
        public IActionResult AutomobileModelList(string sortOrder)
        {
            var models = automobileModelServices.GetAllAutomobileModels().AsQueryable();

            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            ViewData["ManufacturerSortParam"] = sortOrder == "manufacturer" ? "manufacturer_desc" : "manufacturer";
            ViewData["YearSortParam"] = sortOrder == "year" ? "year_desc" : "year";
            ViewData["LoadSortParam"] = sortOrder == "load" ? "load_desc" : "load";
            ViewData["ConsumptionSortParam"] = sortOrder == "consumption" ? "consumption_desc" : "consumption";

            models = sortOrder switch
            {
                "desc" => models.OrderByDescending(x => x.ModelName),
                "manufacturer" => models.OrderBy(x => x.Manufacturer),
                "manufacturer_desc" => models.OrderByDescending(x => x.Manufacturer),
                "year" => models.OrderBy(x => x.ReleaseYear),
                "year_desc" => models.OrderByDescending(x => x.ReleaseYear),
                "load" => models.OrderBy(x => x.MaxLoad),
                "load_desc" => models.OrderByDescending(x => x.MaxLoad),
                "consumption" => models.OrderBy(x => x.FuelConsumption),
                "consumption_desc" => models.OrderByDescending(x => x.FuelConsumption),
                _ => models.OrderBy(x => x.ModelName)
            };

            return View(models.ToList());
        }

        [HttpGet]
        public IActionResult AddAutomobileModel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAutomobileModel([FromForm] AutomobileModel automobileModel)
        {
            await automobileModelServices.AddAutomobileModel(automobileModel);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditAutomobileModel(int id)
        {
            var placeholder = automobileModelServices.PlaceholderAutomobileModel(id);
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditAutomobileModel([FromForm] AutomobileModel automobileModel)
        {
            if (await automobileModelServices.EditAutomobileModel(automobileModel))
            {
                return RedirectToAction("AutomobileModelList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteAutomobileModel(int id)
        {
            await automobileModelServices.DeleteAutomobileModel(id);

            return RedirectToAction("AutomobileModelList", "Admin");
        }
        // AUTOMOBILE MODEL //


        // CITY //
        public IActionResult CityList(string? sortOrder)
        {
            var cities = cityServices.GetAllCities().AsQueryable();
            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";

            cities = sortOrder switch
            {
                "desc" => cities.OrderByDescending(x => x.Name),
                _ => cities.OrderBy(x => x.Name)
            };

            return View(cities.ToList());
        }

        [HttpGet]
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCity([FromForm] City city)
        {
            await cityServices.AddCity(city);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditCity(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCity([FromForm] City city)
        {
            if (await cityServices.EditCity(city))
            {
                return RedirectToAction("CityList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteCity(int id)
        {
            await cityServices.DeleteCity(id);

            return RedirectToAction("CityList", "Admin");
        }
        // CITY //


        // OFFICE //    
        public IActionResult OfficeList(string sortOrder)
        {
            var offices = officeServices.GetAllOffices().AsQueryable();

            ViewData["NameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            ViewData["CitySortParam"] = sortOrder == "city" ? "city_desc" : "city";

            offices = sortOrder switch
            {
                "desc" => offices.OrderByDescending(x => x.Name),
                "city" => offices.OrderBy(x => x.City.Name),
                "city_desc" => offices.OrderByDescending(x => x.City.Name),
                _ => offices.OrderBy(x => x.Name)
            };

            return View(offices.ToList());
        }

        [HttpGet]
        public IActionResult AddOffice()
        {
            ViewData["CityId"] = new SelectList(cityServices.GetAllCities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice([FromForm] Office office)
        {
            await officeServices.AddOffice(office);
            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditOffice(int id)
        {
            var placeholder = officeServices.PlaceholderOffice(id);
            ViewData["CityId"] = new SelectList(cityServices.GetAllCities(), "Id", "Name");
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditOffice([FromForm] Office office)
        {
            if (await officeServices.EditOffice(office))
            {
                return RedirectToAction("OfficeList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteOffice(int id)
        {
            await officeServices.DeleteOffice(id);

            return RedirectToAction("OfficeList", "Admin");
        }       
        // OFFICE //


        // CLIENT //
        public IActionResult ClientList(string? sortOrder)
        {
            var clients = clientServices.GetAllClients().AsQueryable();

            ViewData["FirstNameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "desc" : "";
            ViewData["LastNameSortParam"] = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewData["GenderSortParam"] = sortOrder == "gender" ? "gender_desc" : "gender";
            ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";

            clients = sortOrder switch
            {
                "desc" => clients.OrderByDescending(x => x.FirstName),
                "lastName" => clients.OrderBy(x => x.LastName),
                "lastName_desc" => clients.OrderByDescending(x => x.LastName),
                "gender" => clients.OrderBy(x=>x.Gender),
                "gender_desc" => clients.OrderByDescending(x=>x.Gender),
                "date" => clients.OrderBy(x => x.DateOfBirth),
                "date_desc" => clients.OrderByDescending(x => x.DateOfBirth),
                _ => clients.OrderBy(x => x.FirstName)
            };

            return View(clients.ToList());
        }

        [HttpGet]
        public IActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromForm] Client client)
        {
            await clientServices.AddClient(client);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditClient(int id)
        {
            var placeholder = clientServices.PlaceholderClient(id);
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditClient([FromForm] Client client)
        {
            if (await clientServices.EditClient(client))
            {
                return RedirectToAction("ClientList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteClient(int id)
        {
            await clientServices.DeleteClient(id);

            return RedirectToAction("ClientList", "Admin");
        }
        // CLIENT //


        // ORDER //
        public IActionResult OrderList(string sortOrder)
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

        [HttpGet]
        public IActionResult AddOrder()
        {
            ViewData["AutomobileId"] = new SelectList(automobileServices.GetAllAutomobiles(), "Id", "AutomobileModelId");
            ViewData["OfficeId"] = new SelectList(officeServices.GetAllOffices(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityServices.GetAllCities(), "Id", "Name");
            ViewData["ClientId"] = new SelectList(clientServices.GetAllClients(), "Id", "Email");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromForm] Order order)
        {
            await orderServices.AddOrder(order);

            return RedirectToAction("AdminPanel", "Admin");
        }

        [HttpGet]
        public IActionResult EditOrder(int id)
        {
            var placeholder = orderServices.PlaceholderOrder(id);
            ViewData["AutomobileId"] = new SelectList(automobileModelServices.GetAllAutomobileModels(), "Id", "ModelName");
            ViewData["OfficeId"] = new SelectList(officeServices.GetAllOffices(), "Id", "Name");
            ViewData["CityId"] = new SelectList(cityServices.GetAllCities(), "Id", "Name");
            ViewData["ClientId"] = new SelectList(clientServices.GetAllClients(), "Id", "Email");
            return View(placeholder);
        }

        [HttpPost]
        public async Task<IActionResult> EditOrder([FromForm] Order order)
        {
            if (await orderServices.EditOrder(order))
            {
                return RedirectToAction("OrderList", "Admin");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            await orderServices.DeleteOrder(id);

            return RedirectToAction("OrderList", "Admin");
        }
        // ORDER //
    }
}
