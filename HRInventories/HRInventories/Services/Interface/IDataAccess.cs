﻿using HRInventories.Models;
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

        Catagory GetCatagorybyID(long Id);

        Catagory UpdateCatagory(Catagory item);

        void DeleteCatagory(Catagory catagory);
    }
}
