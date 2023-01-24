using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Account;
using System.Security.Claims;

namespace PicturyMarket.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> RegisterAsync(RegisterViewModel registerViewModel);
        Task<BaseResponse<ClaimsIdentity>> LoginAsync(LoginViewModel loginViewModel);
        Task<BaseResponse<bool>> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel);
    }
}