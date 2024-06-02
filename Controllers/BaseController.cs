using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
