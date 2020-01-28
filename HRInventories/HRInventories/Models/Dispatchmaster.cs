using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Dispatchmaster
    {
        public Dispatchmaster()
        {
            Dispatchdetails = new HashSet<Dispatchdetails>();
        }

        public long Dispatchid { get; set; }
        public DateTime Dispatchdate { get; set; }
        public long Employeeid { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual ICollection<Dispatchdetails> Dispatchdetails { get; set; }
    }
}
