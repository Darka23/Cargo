using Microsoft.AspNetCore.Mvc;

namespace Cargo.Areas.Ofis.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
