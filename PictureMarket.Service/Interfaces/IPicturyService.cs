using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.ViewModels.Pictury;

namespace PicturyMarket.Service.Interfaces
{
    public interface IPicturyService
    {
        Task<IBaseResponse<IEnumerable<Pictury>>> GetPicturies();
        Task<IBaseResponse<Pictury>> GetPictury(int id);
        Task<IBaseResponse<Pictury>> GetPicturyByTitle(string title);
        Task<IBaseResponse<bool>> DeletePicrury(int id);
        Task<IBaseResponse<PicturyViewModel>> CreatePictury(PicturyViewModel picturyViewModel);
    }
}