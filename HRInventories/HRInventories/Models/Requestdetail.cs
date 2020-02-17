using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Requestdetail
    {
        public long Requestdetailid { get; set; }
        public long Requestid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Status { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual Product Product { get; set; }
        public virtual Reqestmaster Request { get; set; }
    }
}
