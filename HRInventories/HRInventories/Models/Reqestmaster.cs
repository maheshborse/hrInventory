using System;
using System.Collections.Generic;

namespace HRInventories.Models
{
    public partial class Reqestmaster
    {
        public Reqestmaster()
        {
            Requestdetail = new HashSet<Requestdetail>();
        }

        public long Requestid { get; set; }
        public string Employeeid { get; set; }
        public bool Isread { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual ICollection<Requestdetail> Requestdetail { get; set; }
    }
}
