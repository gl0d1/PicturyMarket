using Microsoft.EntityFrameworkCore;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using System.Collections.Generic;

namespace PicturyMarket.DAL.Repositories
{
    public class PicturyRepository : IPicturyRepository
    {
        private readonly PicturyMarketDbContext _db;

        public PicturyRepository(PicturyMarketDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Pictury entity)
        {
            await _db.pictures.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Pictury entity)
        {
            _db.pictures.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Pictury>> Select()
        {
            return await _db.pictures.ToListAsync();
        }

        public async Task<bool> Update(Pictury entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Pictury> GetByName(string name)
        {
            return await _db.pictures.FirstOrDefaultAsync(pictury => pictury.name == name);
        }

        public async Task<Pictury> Get(int id)
        {
            return await _db.pictures.FirstOrDefaultAsync(pictury => pictury.id == id);
        }
    }
}
