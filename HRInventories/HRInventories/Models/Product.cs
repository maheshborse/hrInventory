using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Product
    {
        public Product()
        {
            Podetail = new HashSet<Podetail>();
            Requestdetail = new HashSet<Requestdetail>();
        }

        public int Productid { get; set; }
        public long Categoryid { get; set; }
        public string Productname { get; set; }
        public string Productdescription { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual Catagory Category { get; set; }
        public virtual ICollection<Podetail> Podetail { get; set; }
        public virtual ICollection<Requestdetail> Requestdetail { get; set; }
    }
}
