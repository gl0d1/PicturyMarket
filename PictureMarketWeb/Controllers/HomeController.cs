using Microsoft.AspNetCore.Mvc;

namespace PicturyMarketWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}