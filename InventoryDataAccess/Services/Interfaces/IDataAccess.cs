using InventoryDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryDataAccess.Services.Interfaces
{
    public interface IDataAccess
    {
        Task AddCategory(Category Catogory);
    }
}
    

