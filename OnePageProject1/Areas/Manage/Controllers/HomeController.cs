using Microsoft.AspNetCore.Mvc;

namespace OnePageProject1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();
    }
}
