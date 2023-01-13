using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task<IActionResult> GetPicturies()
        {
            var response = await _picturyService.GetPicturies();

            if (response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Date);
            }

            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetPictury(int id)
        {
            var response = await _picturyService.GetPictury(id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Date);
            }

            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePicruty(int id)
        {
            var response = await _picturyService.DeletePicrury(id);

            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetPicturies");
            }

            return RedirectToAction("Error");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if(id == 0)
            {
                return View();
            }

            var response = await _picturyService.GetPictury(id);
            
            if(response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Date);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(PicturyViewModel picturyViewModel)
        {
            if(ModelState.IsValid)
            {
                if(picturyViewModel.Id == 0)
                {
                    await _picturyService.CreatePictury(picturyViewModel);
                }
                else
                {
                    await _picturyService.EditPictury(picturyViewModel.Id,picturyViewModel);
                }
            }

            return RedirectToAction("GetPicturies");
        }
    }
}
