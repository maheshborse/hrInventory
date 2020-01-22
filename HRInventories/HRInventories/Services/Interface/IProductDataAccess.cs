using HRInventories.Models;
using HRInventories.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IProductDataAccess
    {
        Task AddProduct(ProductModel product);

        Task<List<ProductModel>> GetProducts();

        Product GetProductbyID(long id);

        Product UpdateProduct(Product item);

        void DeleteProduct(Product product);
    }
}
