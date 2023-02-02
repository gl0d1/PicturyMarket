using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Service.Interfaces;
using PicturyMarket.Domain.Extensions;
using PicturyMarket.Domain.ViewModels.User;

namespace PicturyMarketWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsersAsync();

            if (response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _userService.CreateAsync(userViewModel);

                if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
                {
                    return Json(new {description = response.Description});
                }

                return BadRequest(new {errorMessage = response.Description});
            }
            
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();

            return StatusCode(StatusCodes.Status500InternalServerError, new {errorMessage});
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            var types = _userService.GetRoles();
            return Json(types.Data);
        }
    }
}