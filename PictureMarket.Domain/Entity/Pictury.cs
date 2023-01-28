using PicturyMarket.Domain.Enum;

namespace PicturyMarket.Domain.Entity
{
    public class Pictury
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public PicturyGenre Genre {get; set; }
        public byte[]? Avatar { get; set; }
    }
}