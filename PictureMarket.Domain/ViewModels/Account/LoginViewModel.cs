using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(25, ErrorMessage = "Имя должно иметь длину меньше 25 символов")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину больше 2 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
