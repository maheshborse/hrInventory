using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class CatagoryModel
    {
        public long Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categorydescription { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
    }
}
