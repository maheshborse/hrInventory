using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class DispatchmasterModel
    {
        public long Dispatchid { get; set; }
        public DateTime Dispatchdate { get; set; }
        public long Employeeid { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public List<DispatchdetailsModel> DispatchdetailsModels { get; set; }
    }
}
