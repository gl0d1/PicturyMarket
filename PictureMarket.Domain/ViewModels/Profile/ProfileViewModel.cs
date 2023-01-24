using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите возраст")]
        [Range(0,130, ErrorMessage = "Диапазон возраста должен быть от 0 до 130")]
        public byte Age { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string UserName { get; set; }
    }
}
