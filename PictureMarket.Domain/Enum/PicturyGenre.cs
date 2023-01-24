using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.Enum
{
    public enum PicturyGenre
    {
        [Display(Name = "Портрет")]
        Portriet = 0,
        [Display(Name = "Пейзаж")]
        Scenery = 1,
        [Display(Name = "Морина")]
        Morina = 2,
        [Display(Name = "Анималистический")]
        Animalistic = 3,
        [Display(Name = "Натюрморт")]
        StillLife = 4,
        [Display(Name = "Батальный")]
        Battle = 5,
    }
}