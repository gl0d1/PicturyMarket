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
            await _db.Picturies.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Pictury entity)
        {
            _db.Picturies.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Pictury>> Select()
        {
            return await _db.Picturies.ToListAsync();
        }

        public async Task<Pictury> Update(Pictury entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Pictury> GetByTitle(string title)
        {
            return await _db.Picturies.FirstOrDefaultAsync(pictury => pictury.Title == title);
        }

        public async Task<Pictury> Get(int id)
        {
            return await _db.Picturies.FirstOrDefaultAsync(pictury => pictury.Id == id);
        }
    }
}
