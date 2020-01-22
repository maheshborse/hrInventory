using HRInventories.Models;
using HRInventories.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class PomasterDataAccess: IPomasterDataAccess
    {
        Connectionstrings _connectionstring;
        public PomasterDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public async Task AddPomaster(Pomaster pomaster)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Pomaster pomasters = new Pomaster()
                    {
                        // removed DisplayName from UI, so will set display name as unit name
                        Podate = pomaster.Podate,
                        Totalamount = pomaster.Totalamount,
                        Discount = pomaster.Discount,
                        Finalamount = pomaster.Finalamount,
                        Userid = pomaster.Userid,
                        Createddate = pomaster.Createddate,
                        Isdeleted = pomaster.Isdeleted
                    };
                    await context.Pomaster.AddAsync(pomasters);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<List<Pomaster>> GetPomasters()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.Pomaster.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Pomaster GetPomasterbyID(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                return context.Pomaster.FirstOrDefault(e => e.Poid == id);
            }
        }
        public Pomaster UpdatePomaster(Pomaster item)
        {
            var dbPomaster = new Pomaster();
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                dbPomaster = context.Pomaster.Where(k => k.Poid == item.Poid).FirstOrDefault();
                dbPomaster.Podate = item.Podate;
                dbPomaster.Totalamount = item.Totalamount;
                dbPomaster.Discount = item.Discount;
                dbPomaster.Finalamount = item.Finalamount;
                dbPomaster.Userid = item.Userid;
                dbPomaster.Createddate = item.Createddate;
                dbPomaster.Isdeleted = item.Isdeleted;
                context.SaveChanges();
            }
            return dbPomaster;
        }
        public void DeletePomaster(Pomaster pomaster)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                context.Pomaster.Remove(pomaster);
                context.SaveChanges();
            }
        }
    }
}
