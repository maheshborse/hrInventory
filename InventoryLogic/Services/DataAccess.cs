
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryLogic.Services
{
    public class DataAccess
    {
        ConnectionString _connectionstring;
        public DataAccess(ConnectionString connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public async Task AddInsertCategory()
        {
            using (DataContext context = new DataContext(_connectionstring))
            {
                Category catogory = new Category()
                {
                    // removed DisplayName from UI, so will set display name as unit name
                    CreatedTime = DateTime.Now,
                    DisplayName = unit.Name,
                    IsDeleted = false,
                    Name = unit.Name,
                    OrgId = organizationId,
                    UpdatedTime = DateTime.Now
                };
                await context.Units.AddAsync(dbUnit);
                await context.SaveChangesAsync();
            }
        }
    }
}
