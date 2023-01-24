using PicturyMarket.Domain.Enum;


namespace PicturyMarket.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set;}
        public Role Role { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Pictury>? Picturies { get; set; }
        public Basket Basket { get; set; }
    }
}