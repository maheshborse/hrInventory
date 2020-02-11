using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class RequestDetailModel
    {
        public int Requestdetailid { get; set; }
        public int Requestid { get; set; }
        public int Productid { get; set; }
        public long Quantity { get; set; }
        public string Status { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public ProductModel ProductModels { get; set; }
    }
}
