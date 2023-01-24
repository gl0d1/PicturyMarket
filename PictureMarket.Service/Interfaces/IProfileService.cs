using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Profile;

namespace PicturyMarket.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfileAsync(string userName);
        Task<BaseResponse<Profile>> SaveAsync(ProfileViewModel profileViewModel);
    }
}