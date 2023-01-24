using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.ViewModels.Pictury
{
    public class PicturyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название картины")]
        [MinLength(1,ErrorMessage = "Минимальная длина должна быть больше 1 символа")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание картины")]
        public string Description { get; set; }

        [Display(Name = "Стоймость")]
        [Required(ErrorMessage = "Укажите стоймотсь картины")]
        public decimal Price { get; set; }

        public string DateCreate { get; set; }

        [Display(Name = "Жанр")]
        [Required(ErrorMessage = "Выберите жанр")]
        public string Genre { get; set; }

        public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }
    }
}
