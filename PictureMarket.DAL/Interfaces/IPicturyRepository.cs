﻿using PicturyMarket.Domain.Entity;

namespace PicturyMarket.DAL.Interfaces
{
    public interface IPicturyRepository: IBaseRepository<Pictury>  
    {
        Task<Pictury> GetByTitle(string title);
    }
}
