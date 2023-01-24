using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly PicturyMarketDbContext _db;
        public BasketRepository(PicturyMarketDbContext db) 
        {
            _db = db;
        }

        public async Task CreateAsync(Basket entity)
        {
            await _db.Baskets.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Basket entity)
        {
            _db.Baskets.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Basket> GetAll()
        {
            return _db.Baskets;
        }

        public async Task<Basket> UpdateAsync(Basket entity)
        {
            _db.Baskets.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
