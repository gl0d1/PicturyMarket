using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Profile;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarket.Service.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly IBaseRepository<Profile> _profileRepository;

        public ProfileService(ILogger<ProfileService> logger, IBaseRepository<Profile> baseRepository)
        {
            _logger = logger;
            _profileRepository = baseRepository;
        }

        public async Task<BaseResponse<ProfileViewModel>> GetProfileAsync(string userName)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        Id = x.Id,
                        Age= x.Age,
                        UserName= userName,
                    })
                    .FirstOrDefaultAsync(x => x.UserName== userName);

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[ProfileSrvice.GetProfileAsync] error: {exception.Message}");

                return new BaseResponse<ProfileViewModel>()
                {
                    Description = $"Внутренняя ошибка: {exception.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<Profile>> SaveAsync(ProfileViewModel profileViewModel)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == profileViewModel.Id);

                profile.Age = profileViewModel.Age;

                await _profileRepository.UpdateAsync(profile);

                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    Description = "Данные обновлены",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[ProfileService.SaveAsync] error: {exception.Message}");

                return new BaseResponse<Profile>()
                {
                    Description = $"Внутренняя ошибка: {exception.Message}",
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
    }
}