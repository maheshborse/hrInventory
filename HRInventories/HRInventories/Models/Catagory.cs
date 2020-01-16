using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Catagory
    {
        public long Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categorydescription { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
    }
}
