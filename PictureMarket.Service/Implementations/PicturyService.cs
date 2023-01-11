using PictureMarket.Service.Interfaces;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Pictury;
using System;

namespace PictureMarket.Service.Implementations
{
    public class PicturyService : IPicturyService
    {
        private readonly IPicturyRepository _picturyRepository;

        public PicturyService(IPicturyRepository picturyRepository)
        {
            _picturyRepository = picturyRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Pictury>>> GetPictures()
        {
            var baseResponse = new BaseResponse<IEnumerable<Pictury>>();
            
            try
            {
                var pictures = await _picturyRepository.Select();

                if (pictures.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;

                    return baseResponse;
                }

                baseResponse.Date = pictures;
                baseResponse.StatusCode = StatusCode.OK; 

                return baseResponse;
            }
            catch(Exception exception) 
            {
                return new BaseResponse<IEnumerable<Pictury>>()
                { 
                    Description = $"[GetPictures] : {exception.Message}",
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

        public async Task<IBaseResponse<Pictury>> GetPicturyByName(string name)
        {
            var baseResponse = new BaseResponse<Pictury>();

            try
            {
                var pictury = await _picturyRepository.GetByName(name);

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
                    Description = $"[GetPicturyByName] : {exception.Message}",
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
                    name = picturyViewModel.name,
                    description = picturyViewModel.description,
                    image_Url = picturyViewModel.image_Url,
                    price = picturyViewModel.price ,
                    data_create = DateTime.Now ,
                    genre = (PicturyGenre)Convert.ToInt32(picturyViewModel.genre)
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