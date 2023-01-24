using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Helpers;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.Account;
using PicturyMarket.Service.Interfaces;
using System.Security.Claims;

namespace PicturyMarket.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly IBaseRepository<User > _userRepository;
        private readonly IBaseRepository<Basket> _basketRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<Profile> profileRepository, IBaseRepository<User> userRepository, IBaseRepository<Basket> basketRepository, ILogger<AccountService> logger)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == changePasswordViewModel.UserName);

                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                user.Password = HashPasswordHelper.HashPassword(changePasswordViewModel.NewPassword);
                await _userRepository.UpdateAsync(user);

                return new BaseResponse<bool>
                {
                    Data = true,
                    Description = "Пароль обнавлён",
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[ChangePasswordAsync]: {exception.Message}");

                return new BaseResponse<bool>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> LoginAsync(LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == loginViewModel.Name);

                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                if(user.Password != HashPasswordHelper.HashPassword(loginViewModel.Password))
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "Неверный пароль или логин"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[LoginAsync]: {exception.Message}");

                return new BaseResponse<ClaimsIdentity>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> RegisterAsync(RegisterViewModel registerViewModel)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == registerViewModel.Name);

                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity> 
                    {
                        Description = "Пользоваетль с таким логином уже есть",
                    };
                }

                user = new User()
                {
                    Name = registerViewModel.Name,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassword(registerViewModel.Password),
                };

                await _userRepository.CreateAsync(user);

                var profile = new Profile()
                {
                    UserId = user.Id,
                };

                var basket = new Basket()
                {
                    UserId = user.Id,
                };

                await _profileRepository.CreateAsync(profile);
                await _basketRepository.CreateAsync(basket);

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "Объект добавился",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[RegisterAsync]: {exception.Message}");

                return new BaseResponse<ClaimsIdentity>
                {
                    Description = exception.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };

            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}