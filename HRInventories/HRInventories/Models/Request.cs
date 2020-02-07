using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Request
    {
        public int Requestid { get; set; }
        public long Productid { get; set; }
        public long Quantity { get; set; }
        public string Status { get; set; }
        public bool Isread { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
    }
}
