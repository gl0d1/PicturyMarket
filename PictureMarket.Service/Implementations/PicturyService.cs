using PicturyMarket.Service.Interfaces;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Pictury;
using PicturyMarket.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace PicturyMarket.Service.Implementations
{
    public class PicturyService : IPicturyService
    {
        private readonly IBaseRepository<Pictury> _picturyRepository;

        public PicturyService(IBaseRepository<Pictury> picturyRepository)
        {
            _picturyRepository = picturyRepository;
        }

        public async Task<IBaseResponse<Pictury>> CreatePicturyAsync(PicturyViewModel picturyViewModel, byte[] imageData)
        {
            try
            {
                var pictury = new Pictury()
                {
                    Title = picturyViewModel.Title,
                    Description = picturyViewModel.Description,
                    DateCreate = DateTime.Now,
                    Genre = (PicturyGenre)Convert.ToInt32(picturyViewModel.Genre),
                    Price = picturyViewModel.Price,
                    Avatar = imageData,
                };

                await _picturyRepository.CreateAsync(pictury);

                return new BaseResponse<Pictury>()
                {
                    Data = pictury,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception) 
            { 
                return new BaseResponse<Pictury>()
                {
                    Description = $"[CreateAsync] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePicruryAsync(int id)
        {
            try
            {
                var pictury = await _picturyRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if(pictury == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _picturyRepository.DeleteAsync(pictury);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePictury] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Pictury>> EditPicturyAsync(int id, PicturyViewModel picturyViewModel)
        {
            try
            {
                var pictury = await _picturyRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (pictury == null)
                {
                    return new BaseResponse<Pictury>()
                    {
                        Description = "Pictury not found",
                        StatusCode = StatusCode.PicturyNotFound
                    };
                }

                pictury.Description = picturyViewModel.Description;
                pictury.Price = picturyViewModel.Price;
                pictury.DateCreate = DateTime.ParseExact(picturyViewModel.DateCreate,"yyyyMMdd HH:mm", null);
                pictury.Title = picturyViewModel.Title;

                await _picturyRepository.UpdateAsync(pictury);


                return new BaseResponse<Pictury>()
                {
                    Data = pictury,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Pictury>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetGenres()
        {
            try
            {
                var genres = ((PicturyGenre[])Enum.GetValues(typeof(PicturyGenre)))
                    .ToDictionary(k => (int) k, g => g.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = genres,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = $"[GetGenres] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Pictury>> GetPicturies()
        {
            try
            {
                var picturies = _picturyRepository.GetAll().ToList();

                if (!picturies.Any())
                {
                    return new BaseResponse<List<Pictury>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Pictury>>()
                {
                    Data = picturies,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exceptiion)
            {
                return new BaseResponse<List<Pictury>>()
                {
                    Description = $"[GetPicturies] : {exceptiion.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PicturyViewModel>> GetPicturyAsync(int id)
        {
            try
            {
                var pictury = await _picturyRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if(pictury == null)
                {
                    return new BaseResponse<PicturyViewModel>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new PicturyViewModel()
                {
                    Title = pictury.Title,
                    Description = pictury.Description,
                    DateCreate = pictury.DateCreate.ToLongDateString(),
                    Price = pictury.Price,
                    Genre = pictury.Genre.GetDisplayName(),
                    Image = pictury.Avatar,
                };

                return new BaseResponse<PicturyViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                return new BaseResponse<PicturyViewModel>()
                {
                    Description = $"[GetPicturyAsync] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        
        public async Task<BaseResponse<Dictionary<int, string>>> GetPicturyAsync(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>(); 
            try
            {
                var picturies = await _picturyRepository.GetAll()
                    .Select(x => new PicturyViewModel()
                    {
                        Title = x.Title,
                        Description = x.Description,
                        DateCreate = x.DateCreate.ToLongDateString(),
                        Price = x.Price,
                        Genre= x.Genre.GetDisplayName(),
                    })
                    .Where(x => EF.Functions.Like(x.Title, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Title);

                baseResponse.Data = picturies;
                return baseResponse;
            }
            catch(Exception exception)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = $"[GetPicturyAsync] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}