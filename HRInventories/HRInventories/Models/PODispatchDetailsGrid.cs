using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Models
{
    public class PODispatchDetailsGrid
    {
        public int Productid { get; set; }
        [Key]
        public int Stock { get; set; }
    }
}
