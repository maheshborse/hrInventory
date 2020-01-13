using HRMSInventoryMangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMSInventoryMangement.Services
{
    public class DataAccess
    {
        private string ConnectionString { get; set; }
        public DataAccess(DatabaseSettings dbSettings)
        {
            ConnectionString = dbSettings.ConnectionString;
        }
    }
}
