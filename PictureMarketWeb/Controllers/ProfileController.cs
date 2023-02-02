using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Domain.ViewModels.Profile;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarketWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel profileViewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserName");

            if(ModelState.IsValid )
            {
                var response = await _profileService.SaveAsync(profileViewModel);

                if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
                {
                    return Json(new {descriptuon = response.Description});
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> Detail()
        {
            var userName = User.Identity.Name;
            var response = await _profileService.GetProfileAsync(userName);
            
            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View();
        }
    }
}
