using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.ViewModels.Pictury;
using PicturyMarket.Domain.Response;

namespace PicturyMarket.Service.Interfaces
{
    public interface IPicturyService
    {
        BaseResponse<Dictionary<int, string>> GetGenres();
        IBaseResponse<List<Pictury>> GetPicturies();
        Task<IBaseResponse<PicturyViewModel>> GetPicturyAsync(int id);
        Task<BaseResponse<Dictionary<int, string>>> GetPicturyAsync(string term);
        Task<IBaseResponse<bool>> DeletePicruryAsync(int id);
        Task<IBaseResponse<Pictury>> CreatePicturyAsync(PicturyViewModel picturyViewModel, byte[] imageData);
        Task<IBaseResponse<Pictury>> EditPicturyAsync(int id, PicturyViewModel picturyViewModel);
    }
}