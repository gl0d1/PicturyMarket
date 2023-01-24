using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PicturyMarket.DAL.Interfaces;
using PicturyMarket.Domain.Entity;
using PicturyMarket.Domain.Enum;
using PicturyMarket.Domain.Extensions;
using PicturyMarket.Domain.Helpers;
using PicturyMarket.Domain.Interfaces;
using PicturyMarket.Domain.Response;
using PicturyMarket.Domain.ViewModels.User;
using PicturyMarket.Service.Interfaces;

namespace PicturyMarket.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<Profile> _profileRepository;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<Profile> profileRepository, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        public async Task<IBaseResponse<User>> CreateAsync(UserViewModel userViewModel)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == userViewModel.Name);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Пользователь с таким логином уже есть",
                        StatusCode = StatusCode.UserAlreadyExists
                    };
                }

                user = new User()
                {
                    Name = userViewModel.Name,
                    Role = Enum.Parse<Role>(userViewModel.Role),
                    Password = HashPasswordHelper.HashPassword(userViewModel.Password),
                };

                await _userRepository.CreateAsync(user);

                var profile = new Profile()
                {
                    Age = 0,
                    UserId = user.Id,
                };

                await _profileRepository.CreateAsync(profile);

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[UserService.CreateAsync] error : {exception.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Data = false,
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                await _userRepository.DeleteAsync(user);

                return new BaseResponse<bool>
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                _logger.LogError(exception, $"[UserService.DeleteUserAsync] error: {exception.Message}");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {exception.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception exception)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = $"Внутренняя ошибка : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role = x.Role.GetDisplayName(),
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsersAsync] получено элементов {users.Count}");

                return new BaseResponse<IEnumerable<UserViewModel>>
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"[UserService.GetUsersAsync] error: {exception.Message}");

                return new BaseResponse<IEnumerable<UserViewModel>>
                {
                    Description = $"Внутренняя ошибка : {exception.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
