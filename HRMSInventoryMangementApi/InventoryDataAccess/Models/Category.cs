using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

using System.ComponentModel.DataAnnotations;
namespace InventoryDataAccess.Models
{
    public class Category
    {
        [Key]
        public long category_id { get; set; }

        public string category_name { get; set; }
        public string category_description { get; set; }
        public int user_id { get; set; }
        public DateTime created_date { get; set; }

        public int isdeleted { get; set; } 



    }
}
