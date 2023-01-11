using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Interfaces;

namespace PictureMarket.Service.Interfaces
{
    public interface IPicturyService
    {
        Task<IBaseResponse<IEnumerable<Pictury>>> GetPictures();
    }
}