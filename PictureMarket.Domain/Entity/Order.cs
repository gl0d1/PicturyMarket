namespace PicturyMarket.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int? PicturyId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? BasketId { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
