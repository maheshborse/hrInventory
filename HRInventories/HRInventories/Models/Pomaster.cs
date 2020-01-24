using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HRInventories.Models
{
    public partial class Pomaster
    {
        public Pomaster()
        {
            Podetail = new HashSet<Podetail>();
        }

        public long Poid { get; set; }
        public DateTime Podate { get; set; }
        public double Totalamount { get; set; }
        public double Discount { get; set; }
        public double Finalamount { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public virtual ICollection<Podetail> Podetail { get; set; }
    }
}
