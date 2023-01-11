using System.ComponentModel.DataAnnotations;

namespace PicturyMarket.Domain.Enum
{
    public enum PicturyGenre
    {
        [Display(Name = "Криминал")]
        Crime = 0,
        [Display(Name = "Детектив")]
        DetectiveFiction = 1,
        [Display(Name = "Научная фантастика")]
        ScienceFiction = 2,
        [Display(Name = "Постапокалипсис")]
        PostApocalyptic = 3,
        [Display(Name = "Антиутопия")]
        Dystopia = 4,
        [Display(Name = "Киберпанк")]
        Cyberpunk = 5,
        [Display(Name = "Фентези")]
        Fantasy = 6,
        [Display(Name = "Любовный роман")]
        RomanceNovel = 7,
        [Display(Name = "Вестерн")]
        Western = 8,
        [Display(Name = "Ужасы")]
        Horror = 9,
    }
}