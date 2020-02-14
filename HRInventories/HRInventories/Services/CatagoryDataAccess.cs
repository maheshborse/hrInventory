using HRInventories.Models;
//using HRInventories.SQLModels;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class CatagoryDataAccess: ICatagoryDataAccess
    {
        Connectionstrings _connectionstring;
        public CatagoryDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public async Task AddCategory(CatagoryModel catogory)
        {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    var flag = await context.Catagory.Where(k=> k.Categoryname == catogory.Categoryname).FirstOrDefaultAsync();
                    if (flag == null)
                    {
                        Catagory Acatogory = new Catagory()
                        {
                            Categoryname = catogory.Categoryname,
                            Categorydescription = catogory.Categorydescription,
                            Userid = catogory.Userid,
                            //Createddate = catogory.Createddate,
                            Isdeleted = catogory.Isdeleted
                        };
                        await context.Catagory.AddAsync(Acatogory);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("Categpry name Already Exit");
                    }
            }
        }

        public async Task<List<Catagory>> GetCategories()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.Catagory.Where(k => k.Isdeleted == "false").ToListAsync();
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Catagory GetCatagorybyID(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                return context.Catagory.FirstOrDefault(e => e.Categoryid == id);
            }
        }
        public Catagory UpdateCatagory(CatagoryModel item)
        {
            var dbCategory = new Catagory();
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                var flag = context.Catagory.Where(k => k.Categoryname == item.Categoryname).FirstOrDefaultAsync();
                if (flag == null)
                {
                    dbCategory = context.Catagory.Where(k => k.Categoryid == item.Categoryid).FirstOrDefault();
                    dbCategory.Categoryname = item.Categoryname;
                    dbCategory.Categorydescription = item.Categorydescription;
                    dbCategory.Userid = item.Userid;
                    //dbCategory.Createddate = item.Createddate;
                    dbCategory.Isdeleted = item.Isdeleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Category name Already Exit");
                }
            }
            return dbCategory;
        }
        public void DeleteCatagory(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                var groupdata= context.Catagory.FirstOrDefault(e => e.Categoryid == id);
                if(groupdata !=null)
                {
                    groupdata.Isdeleted = "true";
                }
                //context.Catagory.Remove(catagory);
                context.SaveChanges();
            }
        }
    }
}
