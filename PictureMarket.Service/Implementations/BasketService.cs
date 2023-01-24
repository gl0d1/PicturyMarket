using Microsoft.EntityFrameworkCore;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Extensions;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Order;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarket.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Pictury> _picturyRepository;

        public BasketService(IBaseRepository<User> userRepository, IBaseRepository<Pictury> picturyRepository)
        {
            _userRepository = userRepository;
            _picturyRepository = picturyRepository;
        }

        public async Task<IBaseResponse<OrderViewModel>> GetItemAsync(string userName, int id)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Name == userName);
                    
                if(user == null)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var orders = user.Basket?.Orders.Where(x => x.Id == id).ToList();

                if(orders == null || orders.Count == 0)
                {
                    return new BaseResponse<OrderViewModel>()
                    {
                        Description = "Заказов нет",
                        StatusCode = StatusCode.OrderNotFound,
                    };
                }

                var response = (from p in orders
                                join c in _picturyRepository.GetAll() on p.PicturyId equals c.Id
                                select new OrderViewModel()
                                {
                                    Id = p.Id,
                                    PicturyTitle = c.Title,
                                    Genre = c.Genre.GetDisplayName(),
                                    Name = p.Name,
                                    Surname = p.Surname,
                                    DateCreate = p.DateCreated.ToLongDateString(),
                                    Image = c.Avatar,
                                }).FirstOrDefault();

                return new BaseResponse<OrderViewModel>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK,
                };
            }
            catch(Exception exception)
            {
                return new BaseResponse<OrderViewModel>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItemsAsync(string userName)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Basket)
                    .ThenInclude(x => x.Orders)
                    .FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
                {
                    return new BaseResponse<IEnumerable<OrderViewModel>>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var orders = user.Basket?.Orders;
                var response = from p in orders
                               join c in _picturyRepository.GetAll() on p.PicturyId equals c.Id
                               select new OrderViewModel()
                               {
                                   Id = p.Id,
                                   PicturyTitle = c.Title,
                                   Genre = c.Genre.GetDisplayName(),
                                   Image = c.Avatar,
                               };

                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception exception)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}