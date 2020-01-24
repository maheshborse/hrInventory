using HRInventories.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Models
{
    public class POViewModel 
    {
        public Pomasters pomastermodel { get; set; }
        public List<Podetails> podetailModel { get; set; }
    }
    public class Pomasters
    {
        public long Poid { get; set; }
        public DateTime Podate { get; set; }
        public double Totalamount { get; set; }
        public double Discount { get; set; }
        public double Finalamount { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

    }
    public class Podetails
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
