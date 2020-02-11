using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Models
{
    public class RequestViewModel
    {
        public Reqestmasters Reqestmastermodel { get; set; }
        public List<Requestdetails> RequestdetailModel { get; set; }
    }
    public class Reqestmasters
    {
        public int Requestid { get; set; }
        public string Employeeid { get; set; }
        public bool Isread { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

    }
    public class Requestdetails
    {
        public int Requestdetailid { get; set; }
        public int Requestid { get; set; }
        public int Productid { get; set; }
        public long Quantity { get; set; }
        public string Status { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

    }
}
