using HRInventories.Models;
using HRInventories.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class DataAccess: IDataAccess
    {
        Connectionstrings _connectionstring;
        public DataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public async Task AddCategory(Catagory catogory)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Catagory Acatogory = new Catagory()
                    {
                        // removed DisplayName from UI, so will set display name as unit name
                        Categoryname = catogory.Categoryname,
                        Categorydescription = catogory.Categorydescription,
                        Userid = catogory.Userid,
                        Createddate = catogory.Createddate,
                        Isdeleted = catogory.Isdeleted
                    };
                    await context.Catagory.AddAsync(Acatogory);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<List<Catagory>> GetCategories()
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                    return await context.Catagory.ToListAsync();
            }
        }
        //public async Task<List<Catagory>> GetCategory(long categoryid)
        //{
        //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //    {
        //        //return await context.Catagory.Where(k => k.Categoryid == categoryid).FirstOrDefaultAsync();
        //    }
        //}

        public Task<List<Catagory>> GetCategory(int categoryid)
        {
            throw new NotImplementedException();
        }
        //public async Task UpdateUnit(Catagory catogory)
        //{
        //    using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
        //    {
        //        var dbUnit = await context.Catagory.Where(k => k.Categoryid == unit.Id && k.OrgId == organizationId).FirstOrDefaultAsync();
        //        if (dbUnit != null)
        //        {
        //            dbUnit.DisplayName = unit.Name; // unit.DisplayName; removed DisplayName from UI, so will set display name as unit name
        //            dbUnit.Name = unit.Name;
        //            //dbUnit.IsDeleted = unit.IsDeleted;
        //            dbUnit.UpdatedTime = DateTime.Now;
        //            await context.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            throw new UnitNotFoundException(organizationId, unit.Id);
        //        }
        //    }
        //}
    }
}
