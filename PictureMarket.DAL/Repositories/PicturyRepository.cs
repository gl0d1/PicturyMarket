using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL.Repositories
{
    public class PicturyRepository : IBaseRepository<Pictury>
    {
        private readonly PicturyMarketDbContext _db;

        public PicturyRepository(PicturyMarketDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Pictury entity)
        {
            await _db.Picturies.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Pictury entity)
        {
            _db.Picturies.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Pictury> GetAll()
        {
            return _db.Picturies;
        }

        public async Task<Pictury> UpdateAsync(Pictury entity)
        {
            _db.Picturies.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}