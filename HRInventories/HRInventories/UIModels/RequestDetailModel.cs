using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class RequestDetailModel
    {
        public long Requestdetailid { get; set; }
        public long Requestid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Status { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public ProductModel ProductModels { get; set; }
    }
}
