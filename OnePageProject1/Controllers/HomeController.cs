using Microsoft.AspNetCore.Mvc;

namespace OnePageProject1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() => View();
    }
}
