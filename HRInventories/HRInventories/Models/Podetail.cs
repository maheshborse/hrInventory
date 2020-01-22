using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HRInventories.Models
{
    public partial class Podetail
    {
        public int Podetailid { get; set; }
        public long Poid { get; set; }
        public int Productid { get; set; }
        public long Quantity { get; set; }
        public double Porate { get; set; }
        public double Discount { get; set; }
        public double Amount { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
       
        public virtual Pomaster Po { get; set; }
        
        public virtual Product Product { get; set; }
    }
}
