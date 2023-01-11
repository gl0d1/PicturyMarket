using PicturyMarket.Domain.Enum;

namespace PicturyMarket.Domain.Entity
{
    public class Pictury
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image_Url { get; set; }
        public decimal price { get; set; }
        public DateTime data_create { get; set; }
        public PicturyGenre genre {get; set; }
    }
}