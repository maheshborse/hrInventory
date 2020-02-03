using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Dispatchdetails
    {
        public int Dispatchdetailid { get; set; }
        public int Dispatchid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual Dispatchmaster Dispatch { get; set; }
    }
}
