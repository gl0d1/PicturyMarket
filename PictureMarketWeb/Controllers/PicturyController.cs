using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using PicturyMarket.Domain.ViewModels.Pictury;

namespace PicturyMarketWeb.Controllers
{
    public class PicturyController : Controller
    {
        private readonly IPicturyService _picturyService;

        public PicturyController(IPicturyService picturyService)
        {
            _picturyService = picturyService;
        }

        [HttpGet]
        public IActionResult GetPicturies()
        {
            var response = _picturyService.GetPicturies();

            if (response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetPicturyAsync(int id)
        {
            var response = await _picturyService.GetPicturyAsync(id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePicrutyAsync(int id)
        {
            var response = await _picturyService.DeletePicruryAsync(id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetPicturies");
            }

            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> SaveAsync(int id)
        {
            if(id == 0)
            {
                return PartialView();
            }

            var response = await _picturyService.GetPicturyAsync(id);
            
            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(PicturyViewModel picturyViewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");

            if(ModelState.IsValid)
            {
                if(picturyViewModel.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(picturyViewModel.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)picturyViewModel.Avatar.Length);
                    }
                    await _picturyService.CreatePicturyAsync(picturyViewModel, imageData);
                }
                else
                {
                    await _picturyService.EditPicturyAsync(picturyViewModel.Id,picturyViewModel);
                }
            }

            return RedirectToAction("GetPicturies");
        }

        [HttpGet]
        public async Task<ActionResult> GetPicturyAsync(int id, bool isJson)
        {
            var response = await _picturyService.GetPicturyAsync(id);

            if(isJson)
            {
                return Json(response.Data);
            }

            return PartialView("GetPicturyAsync", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetPicturyAsync(string term)
        {
            var response = await _picturyService.GetPicturyAsync(term);
            return Json(response.Data);
        }

        [HttpPost]
        public JsonResult GetGenres()
        {
            var genres = _picturyService.GetGenres();
            return Json(genres.Data);
        }

        public IActionResult Compare() => PartialView();
    }
}
