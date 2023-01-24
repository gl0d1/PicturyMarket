using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.ViewModels.Order;

namespace PicturyMarket.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> CreateAsync(CreateOrderViewModel createOrderViewModel);
        Task<IBaseResponse<bool>> DeleteAsync(int id);
    }
}
