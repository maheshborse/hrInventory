﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDataAccess.Models
{
    public class Product
    {
        public int Product_id { get; set; }

        public string Product_name { get; set; }
        public string Product_description { get; set; }
        public int category_id { get; set; }

        public int user_id { get; set; }
        public DateTime created_date { get; set; }

        public int isdeleted { get; set; }
    }
}
