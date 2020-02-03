using HRInventories.Models;
using HRInventories.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class PODispatchDetailsDataAccess: IPODispatchDetailsDataAccess
    {
        Connectionstrings _connectionstring;
        public PODispatchDetailsDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public async Task<List<PODispatchDetailsGrid>> GetPODispatchDetails(int id)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.PODispatchDetailsGrids.ToListAsync();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
