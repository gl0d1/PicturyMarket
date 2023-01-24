using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Количество")]
        [Range(1, 10, ErrorMessage = "Количество должно быть от 1 до 10")]
        public int Quantity { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Укажите имя")]
        [MinLength(2, ErrorMessage = "Имя должно иметь длину больше 2")]
        [MaxLength(25, ErrorMessage = "Имя должно иметь длину меньше 25")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Укажите фамилию")]
        [MinLength(2, ErrorMessage = "Фамилия должна иметь длину больше 2")]
        [MaxLength(50, ErrorMessage = "Фамилия должна иметь длину меньше 50")]
        public string Surname { get; set; }
        public int PicturyId { get; set; }
        public string Login { get; set; }
    }
}
