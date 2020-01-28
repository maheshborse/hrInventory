using HRInventories.Models;
using HRInventories.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface ICatagoryDataAccess
    {
        Task AddCategory(CatagoryModel Catogory);

        Task<List<Catagory>> GetCategories();

        Catagory GetCatagorybyID(long id);

        Catagory UpdateCatagory(CatagoryModel item);

        void DeleteCatagory(long id);
    }
}
