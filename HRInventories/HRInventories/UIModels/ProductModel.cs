using HRInventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class ProductModel
    {
        public int Productid { get; set; }
        public long Categoryid { get; set; }
        public string Productname { get; set; }
        public string Productdescription { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public int stock { get; set; }
        public CatagoryModel Category { get; set; }
    }
}
