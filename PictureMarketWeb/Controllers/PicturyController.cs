using Microsoft.AspNetCore.Mvc;
using PictureMarket.Service.Interfaces;
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
        public async Task<IActionResult> GetPictures()
        {
            var response = await _picturyService.GetPictures();

            if (response.StatusCode == PicturyMarket.Domain.Enum.StatusCode.OK)
            {
                return View(response.Date);
            }

            return RedirectToAction("Error");
        }

    }
}
