using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Domain.ViewModels.Order;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarketWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult CreateOrder(int id) 
        {
            var orderViewModel = new CreateOrderViewModel()
            {
                PicturyId = id,
                Login = User.Identity.Name,
                Quantity = 0,
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel createOrderViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _orderService.CreateAsync(createOrderViewModel);
                
                if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
                {
                    return Json(new {description = response.Description});
                }

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _orderService.DeleteAsync(id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Error", $"{response.Description}");
        }
    }
}
