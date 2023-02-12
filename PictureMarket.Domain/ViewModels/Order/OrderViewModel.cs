namespace PicturyMarket.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string PicturyTitle { get; set; }
        public string Genre { get; set; }
        public byte[]? Image { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateCreate { get; set; }
    }
}