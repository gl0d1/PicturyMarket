using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.ViewModels.Order;

namespace PicturyMarket.Service.Interfaces
{
    public interface IBasketService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItemsAsync(string userName);
        Task<IBaseResponse<OrderViewModel>> GetItemAsync(string userName, int id);
    }
}