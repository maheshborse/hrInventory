using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class PomasterModel
    {
        public long Poid { get; set; }
        public DateTime Podate { get; set; }
        public double Totalamount { get; set; }
        public double Discount { get; set; }
        public double Finalamount { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
        public List<PodetailModel> PodetailModels { get; set; }
    }
}
