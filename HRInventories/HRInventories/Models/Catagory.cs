﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HRInventories.Models
{
    public partial class Catagory
    {
        public Catagory()
        {
            Product = new HashSet<Product>();
        }

        public long Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categorydescription { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<Product> Product { get; set; }
    }
}
