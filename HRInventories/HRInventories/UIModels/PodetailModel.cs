using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class PodetailModel
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
       
    }
}
