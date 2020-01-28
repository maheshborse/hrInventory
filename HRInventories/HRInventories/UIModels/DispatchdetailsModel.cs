using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class DispatchdetailsModel
    {
        public long Dispatchdetailid { get; set; }
        public DateTime Dispatchdate { get; set; }
        public long Dispatchid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
    }
}
