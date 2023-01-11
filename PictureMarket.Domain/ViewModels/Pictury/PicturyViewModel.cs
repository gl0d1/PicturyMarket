namespace PicturyMarket.Domain.ViewModels.Pictury
{
    public class PicturyViewModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public string image_Url { get; set; }
        public decimal price { get; set; }
        public DateTime data_create { get; set; }
        public string genre { get; set; }
    }
}
