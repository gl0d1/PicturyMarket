namespace PicturyMarket.Domain.ViewModels.Pictury
{
    public class PicturyViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime DataCreate { get; set; }
        public string Genre { get; set; }
    }
}
