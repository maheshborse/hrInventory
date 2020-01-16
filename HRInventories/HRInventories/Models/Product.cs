using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Product
    {
        public long Productid { get; set; }
        public string Productname { get; set; }
        public string Productdescription { get; set; }
        public string Categoryid { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
    }
}
