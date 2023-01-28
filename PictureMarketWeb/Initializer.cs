using PicturyMarket.DAL.Interfaces;
using PicturyMarket.DAL.Repositories;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Service.Implementations;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarketWeb
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Pictury>, PicturyRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
            services.AddScoped<IBaseRepository<Basket>, BasketRepository>();
            services.AddScoped<IBaseRepository<Order>, OrderRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IPicturyService, PicturyService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProfileService, ProfileService>();
        }
    }
}
