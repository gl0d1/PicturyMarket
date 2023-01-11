namespace PicturyMarket.Domain.Entity
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set;}
        public string Surname { get; set;}
        public DateTime BirthDate { get; set; }
        public ICollection<Pictury>? Picturies { get; set; }
    }
}