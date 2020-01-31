using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Models
{
    public class DispatchViewModel
    {
        public Dispatchmasters DispatchmasterVmodel { get; set; }
        public List<Dispatchdetail> DispatchdetailsVModel { get; set; }
    }
    public class Dispatchmasters
    {
        public long Dispatchid { get; set; }
        public DateTime Dispatchdate { get; set; }
        public long Employeeid { get; set; }
        public string Employeename { get; set; }
        public long Totalqty { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }


    }
    public class Dispatchdetail
    {
        public long Dispatchdetailid { get; set; }
        public long Dispatchid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

    }
}

