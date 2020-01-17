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
        public Catagory GetCatagorybyID(long Id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                return context.Catagory.FirstOrDefault(e => e.Categoryid == Id);
            }
        }
        public void UpdateCatagory(Catagory catagory, Catagory item)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                catagory.Categoryname = item.Categoryname;
                catagory.Categorydescription = item.Categorydescription;
                catagory.Userid = item.Userid;
                catagory.Createddate = item.Createddate;
                catagory.Isdeleted = item.Isdeleted;
                context.SaveChanges();
            }
        }
        public void DeleteCatagory(Catagory catagory)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                context.Catagory.Remove(catagory);
                context.SaveChanges();
            }
        }
    }
}
