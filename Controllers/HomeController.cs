using Microsoft.AspNetCore.Mvc;

namespace FreeFirePanelWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
