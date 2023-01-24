using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturyMarket.Domain.Entity
{
    public class Basket
    {
        public int Id { get; set; }
        public User Users { get; set; }
        public int UserId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
