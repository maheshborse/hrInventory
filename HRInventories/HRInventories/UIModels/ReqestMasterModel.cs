using HRInventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.UIModels
{
    public class ReqestMasterModel
    {
        public int Requestid { get; set; }
        public string Employeeid { get; set; }
        public bool Isread { get; set; }
        public string Userid { get; set; }
        public DateTime Createddate { get; set; }
        public string Isdeleted { get; set; }

        public List<User> users { get; set; }
        public List<RequestDetailModel> requestDetailModels { get; set; }
    }
}
