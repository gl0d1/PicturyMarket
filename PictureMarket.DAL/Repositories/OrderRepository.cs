using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly PicturyMarketDbContext _db;

        public OrderRepository(PicturyMarketDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Order entity)
        {
            await _db.Orders.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            _db.Orders.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            _db.Orders.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
