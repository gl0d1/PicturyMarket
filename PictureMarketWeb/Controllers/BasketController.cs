using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarketWeb.Controllers
{
    public class BasketController : Controller
    {
        private IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Detail()
        {
            var response = await _basketService.GetItemsAsync(User.Identity.Name);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetItem(int id)
        {
            var response = await _basketService.GetItemAsync(User.Identity.Name, id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}