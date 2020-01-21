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
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.Catagory.ToListAsync();
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
        public Catagory UpdateCatagory(Catagory item)
        {
            var dbCategory = new Catagory();
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                dbCategory =  context.Catagory.Where(k => k.Categoryid == item.Categoryid).FirstOrDefault();
                dbCategory.Categoryname = item.Categoryname;
                dbCategory.Categorydescription = item.Categorydescription;
                dbCategory.Userid = item.Userid;
                dbCategory.Createddate = item.Createddate;
                dbCategory.Isdeleted = item.Isdeleted;
                context.SaveChanges();
            }
            return dbCategory;
        }
        public void DeleteCatagory(Catagory catagory)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                context.Catagory.Remove(catagory);
                context.SaveChanges();
            }
        }
        public async Task AddProduct(Product product)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    Product products = new Product()
                    {
                        // removed DisplayName from UI, so will set display name as unit name
                        Productname = product.Productname,
                        Productdescription = product.Productdescription,
                        Userid = product.Userid,
                        Categoryid=product.Categoryid,
                        Createddate = product.Createddate,
                        Isdeleted = product.Isdeleted
                    };
                    await context.Product.AddAsync(products);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public async Task<List<Product>> GetProducts()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.Product.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Product GetProductbyID(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                return context.Product.FirstOrDefault(e => e.Productid == id);
            }
        }
        public void UpdateProduct(Product product, Product item)
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    product.Productname = item.Productname;
                    product.Productdescription = item.Productdescription;
                    product.Userid = item.Userid;
                    product.Createddate = item.Createddate;
                    //catagory.Isdeleted = item.Isdeleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void DeleteProduct(Product product)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                context.Product.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
