using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly PicturyMarketDbContext _db;

        public UserRepository(PicturyMarketDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}