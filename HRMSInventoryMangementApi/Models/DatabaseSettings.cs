using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMSInventoryMangement.Models
{
    public class DatabaseSettings
    {
        public string DbServer { get; set; }
        public string DbName { get; set; } = "IDPTokenizer";
        public string DbPort { get; set; } = "5433";
        public string DbUser { get; set; }
        public string DbPassword { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"Server={this.DbServer};Database={this.DbName};User ID={this.DbUser};Password={this.DbPassword};Port={Convert.ToInt16(this.DbPort)};Integrated Security=false;Timeout=300;CommandTimeout=2400;Enlist=true";
            }
        }
        public static DatabaseSettings InitializeSettings(IConfiguration configuration)
        {
            DatabaseSettings settings = new DatabaseSettings();
            configuration.Bind("Database", settings);

            return settings;
        }
    }
}
