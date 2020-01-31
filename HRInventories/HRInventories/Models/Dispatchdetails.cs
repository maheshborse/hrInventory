using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Dispatchdetails
    {
        public long Dispatchdetailid { get; set; }
        public long Dispatchid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual Dispatchmaster Dispatch { get; set; }
    }
}
