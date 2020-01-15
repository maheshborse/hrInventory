using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryDataAccess.Models
{
    public  class DataContext: DbContext
    {
        ConnectionString _connectionstring;
        public DataContext(DbContextOptions<DataContext> options ) 
            : base(options)
        {

        }
        
        public DbSet<Category> categories { get; set; }
    }
}
