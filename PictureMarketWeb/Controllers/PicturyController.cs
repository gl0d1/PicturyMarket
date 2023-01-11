using Microsoft.AspNetCore.Mvc;
using PicturyMarket.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Enum;


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
    }
}
