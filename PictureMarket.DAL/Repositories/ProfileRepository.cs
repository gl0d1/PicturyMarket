using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly PicturyMarketDbContext _db;

        public ProfileRepository(PicturyMarketDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Profile entity)
        {
            await _db.Profiles.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Profile entity)
        {
            _db.Profiles.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Profile> GetAll()
        {
            return _db.Profiles;
        }

        public async Task<Profile> UpdateAsync(Profile entity)
        {
            _db.Profiles.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
