using HRInventories.Models;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services
{
    public class ProductDataAccess: IProductDataAccess
    {
        Connectionstrings _connectionstring;
        public ProductDataAccess(Connectionstrings connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public async Task AddProduct(ProductModel product)
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
                        Categoryid = product.Categoryid,
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
        public async Task<List<ProductModel>> GetProducts()
        {
            try
            {
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    return await context.Product.Select(p => new ProductModel
                    {
                        Productid = p.Productid,
                        Productname = p.Productname,
                        Productdescription = p.Productdescription,
                        Categoryid = p.Categoryid,
                        Userid = p.Userid,
                        Isdeleted = p.Isdeleted,
                        Category = new CatagoryModel
                        {
                            Categoryname = p.Category.Categoryname,
                            Categorydescription = p.Category.Categorydescription,
                            Createddate = p.Category.Createddate,
                            Isdeleted = p.Category.Isdeleted,
                            Categoryid = p.Category.Categoryid,
                        },
                        balance = context.PODispatchDetailsGrids.Where(k=> k.productid == p.Productid).Select(b=> b.balance).FirstOrDefault(),
                    }).Where(k=> k.Isdeleted == "false").ToListAsync();
                    

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
        public Product UpdateProduct(Product item)
        {
            try
            {
                var dbCategory = new Product();
                using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
                {
                    dbCategory = context.Product.Where(k => k.Productid == item.Productid).FirstOrDefault();
                    dbCategory.Productname = item.Productname;
                    dbCategory.Productdescription = item.Productdescription;
                    dbCategory.Userid = item.Userid;
                    dbCategory.Createddate = item.Createddate;
                    dbCategory.Isdeleted = item.Isdeleted;
                    context.SaveChanges();
                }
                return dbCategory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void DeleteProduct(long id)
        {
            using (HRInventoryDBContext context = new HRInventoryDBContext(_connectionstring))
            {
                var groupdata = context.Product.FirstOrDefault(e => e.Productid == id);
                if (groupdata != null)
                {
                    groupdata.Isdeleted = "true";
                }
                //context.Product.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
