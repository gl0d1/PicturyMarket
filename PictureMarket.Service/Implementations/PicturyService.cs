using PicturyMarket.Service.Interfaces;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Pictury;
using System;

namespace PicturyMarket.Service.Implementations
{
    public class PicturyService : IPicturyService
    {
        private readonly IPicturyRepository _picturyRepository;

        public PicturyService(IPicturyRepository picturyRepository)
        {
            _picturyRepository = picturyRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Pictury>>> GetPicturies()
        {
            var baseResponse = new BaseResponse<IEnumerable<Pictury>>();
            
            try
            {
                var picturies = await _picturyRepository.Select();

                if (picturies.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;

                    return baseResponse;
                }

                baseResponse.Date = picturies;
                baseResponse.StatusCode = StatusCode.OK; 

                return baseResponse;
            }
            catch(Exception exception) 
            {
                return new BaseResponse<IEnumerable<Pictury>>()
                { 
                    Description = $"[GetPicturies] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Pictury>> GetPictury(int id)
        {
            var baseResponse = new BaseResponse<Pictury>();

            try
            {
                var pictury = await _picturyRepository.Get(id);

                if(pictury == null) 
                {
                    baseResponse.Description = "Pictury not found";
                    baseResponse.StatusCode = StatusCode.PicturyNotFound;

                    return baseResponse;
                }

                baseResponse.Date = pictury;

                return baseResponse;
            }
            catch(Exception exception)
            {
                return new BaseResponse<Pictury>()
                {
                    Description = $"[GetPictury] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Pictury>> GetPicturyByTitle(string title)
        {
            var baseResponse = new BaseResponse<Pictury>();

            try
            {
                var pictury = await _picturyRepository.GetByTitle(title);

                if (pictury == null)
                {
                    baseResponse.Description = "Pictury not found";
                    baseResponse.StatusCode = StatusCode.PicturyNotFound;

                    return baseResponse;
                }

                baseResponse.Date = pictury;

                return baseResponse;
            }
            catch (Exception exception)
            {
                return new BaseResponse<Pictury>()
                {
                    Description = $"[GetPicturyByTitle] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePicrury(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var pictury = await _picturyRepository.Get(id);

                if(pictury == null)
                {
                    baseResponse.Description = "Pictury not found";
                    baseResponse.StatusCode = StatusCode.PicturyNotFound;

                    return baseResponse;
                }

                await _picturyRepository.Delete(pictury);

                return baseResponse;
            }
            catch(Exception exception)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeletePictury] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PicturyViewModel>> CreatePictury(PicturyViewModel picturyViewModel)
        {
            var baseResponse = new BaseResponse<PicturyViewModel>();

            try
            {
                var pictury = new Pictury()
                {
                    Title = picturyViewModel.Title,
                    Description = picturyViewModel.Description,
                    ImageUrl = picturyViewModel.ImageUrl,
                    Price = picturyViewModel.Price ,
                    DataCreate = DateTime.Now ,
                    Genre = (PicturyGenre)Convert.ToInt32(picturyViewModel.Genre)
                };

                await _picturyRepository.Create(pictury);
            }
            catch(Exception exception)
            {
                return new BaseResponse<PicturyViewModel>()
                {
                    Description = $"[CreatePictury] : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            return baseResponse;
        }
    }
}