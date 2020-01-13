using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMSInventoryMangement.Models
{
    public class Category
    {
        public int category_id { get; set; }

        public string category_name { get; set; }
        public string category_description { get; set; }
        public int user_id { get; set; }
        public DateTime created_date { get; set; }

        public int isdeleted { get; set; } 



    }
}
