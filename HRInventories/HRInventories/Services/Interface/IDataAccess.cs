using HRInventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IDataAccess
    {
        Task AddCategory(Catagory Catogory);

        Task<List<Catagory>> GetCategories();

        Catagory GetCatagorybyID(long id);

        Catagory UpdateCatagory(Catagory item);

        void DeleteCatagory(Catagory catagory);

        Task AddProduct(Product product);

        Task<List<Product>> GetProducts();

        Product GetProductbyID(long id);

        Product UpdateProduct(Product item);

        void DeleteProduct(Product product);
    }
}
