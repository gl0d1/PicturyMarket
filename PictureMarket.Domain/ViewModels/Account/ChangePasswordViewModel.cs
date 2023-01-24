using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен быть больше 6 символов")]
        public string NewPassword { get; set; }
    }
}
