using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Domain.ViewModels.Account;
using PicturyMarket.Service.Interfaces;
using System.Security.Claims;

namespace PicturyMarketWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult RegisterAsync() => View();

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _accountService.RegisterAsync(registerViewModel);

                if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", response.Description);
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult LoginAsync() => View();

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = await _accountService.LoginAsync(loginViewModel);

                if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(loginViewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");        
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePasswordAsync(changePasswordViewModel);

                if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
                {
                    return Json(new {description = response.Description});
                }
            }

            var modelError = ModelState.Values.SelectMany(v => v.Errors);

            return StatusCode(StatusCodes.Status500InternalServerError, new {modelError.FirstOrDefault().ErrorMessage});
        }
    }
}