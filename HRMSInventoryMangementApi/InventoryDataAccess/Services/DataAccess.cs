
using InventoryDataAccess.Models;
using InventoryDataAccess.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDataAccess.Services
{
    public class DataAccess: IDataAccess
    {
        ConnectionString _connectionstring;
        public DataAccess(ConnectionString connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public async Task AddCategory(Category catogory)
        {
            using (DataContext context = new DataContext(_connectionstring))
            {
                Category Acatogory = new Category()
                {
                    // removed DisplayName from UI, so will set display name as unit name
                    category_name= catogory.category_name, category_description=catogory.category_description, user_id=catogory.user_id, created_date=catogory.created_date
                };
                await context.category.AddAsync(Acatogory);
                await context.SaveChangesAsync();

            }
        }
        //public async Task<List<Category>> GetCategory(int categoryid)
        //{
        //    using (DataContext context = new DataContext(_connectionstring))
        //    {

        //        //return await context.category.Where(k => k.category_id == categoryid).;
        //    }
        //}
        //public async Task UpdateCategory(Category catogory)
        //{
        //    using (DataContext context = new DataContext(_connectionstring))
        //    {
        //        //var dbcategory = await context.category.Where(k => k.category_id ==catogory.category_id).FirstOrDefaultAsync();
        //        if (dbcategory != null)
        //        {
        //            dbcategory.category_name = catogory.category_name; // unit.DisplayName; removed DisplayName from UI, so will set display name as unit name
        //            dbcategory.category_description = catogory.category_description;
        //            //dbUnit.IsDeleted = unit.IsDeleted;
        //            dbcategory.user_id = catogory.user_id;
        //            await context.SaveChangesAsync();
        //        }
                
        //    }
        //}
    }
}
