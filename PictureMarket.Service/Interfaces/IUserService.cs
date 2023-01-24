using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.User;

namespace PicturyMarket.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> CreateAsync(UserViewModel userViewModel);
        BaseResponse<Dictionary<int, string>> GetRoles();
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync();
        Task<IBaseResponse<bool>> DeleteUserAsync(int id);
    }
}
