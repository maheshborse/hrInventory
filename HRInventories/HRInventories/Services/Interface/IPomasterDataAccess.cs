using HRInventories.Models;
using HRInventories.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IPomasterDataAccess
    {
        Task AddPo(POViewModel pOViewModel);

        Task<List<PomasterModel>> GetPo();

        //Pomaster GetPobyID(long id);

        Task UpdatePo(POViewModel pOViewModel);

        Task DeletePo(long poid);
    }
}
