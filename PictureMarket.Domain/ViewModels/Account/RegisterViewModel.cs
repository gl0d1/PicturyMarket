using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(25, ErrorMessage = "Имя должно иметь длину меньше 25 символов")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину больше 2 символов")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Укажите пароль")]
        [MinLength(6,ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set;}
    }
}
