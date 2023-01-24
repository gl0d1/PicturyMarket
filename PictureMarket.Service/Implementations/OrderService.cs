using Microsoft.EntityFrameworkCore;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Order;
using PicturyMarket.Service.Interfaces;
using PicturyMarket.Domain.Interfaces;

namespace PicturyMarket.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Order> _orderRepository;

        public OrderService(IBaseRepository<User> userRepository, IBaseRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IBaseResponse<Order>> CreateAsync(CreateOrderViewModel createOrderViewModel)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .FirstOrDefaultAsync(x => x.Name == createOrderViewModel.Login);

                if (user == null)
                {
                    return new BaseResponse<Order>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var order = new Order()
                {
                    Name = createOrderViewModel.Name,
                    Surname= createOrderViewModel.Surname,
                    DateCreated = DateTime.Now,
                    BasketId = user.Basket.Id,
                    PicturyId = createOrderViewModel.PicturyId,
                };

                await _orderRepository.CreateAsync(order);

                return new BaseResponse<Order>()
                {
                    Description = "Заказ создан",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                return new BaseResponse<Order>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var order = _orderRepository.GetAll()
                    .Select(x => x.Basket.Orders.FirstOrDefault(y => y.Id == id))
                    .FirstOrDefault();

                if(order== null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Заказ не найден",
                        StatusCode = StatusCode.OrderNotFound
                    };
                }

                await _orderRepository.DeleteAsync(order);

                return new BaseResponse<bool>()
                {
                    Description = "Заказ удалён",
                    StatusCode = StatusCode.OK,
                };
            }
            catch(Exception exception)
            {
                return new BaseResponse<bool>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}
