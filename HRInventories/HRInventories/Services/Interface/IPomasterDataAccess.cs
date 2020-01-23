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
        //Task AddPo(PodetailModel podetail);

        Task AddPo(POViewModel pOViewModel);

        //Task<List<POViewModel>> GetPomasters();


        //Pomaster GetPomasterbyID(long id);

        //Pomaster UpdatePomaster(Pomaster item);

        //void DeletePomaster(Pomaster pomaster);
    }
}
