using HRInventories.Models;
using HRInventories.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IdispatchDataAccess
    {
        Task Adddispatch(DispatchViewModel dispatchViewModel);

        Task<List<DispatchmasterModel>> Getdispatch();

        Task UpdateDispatch(DispatchViewModel dispatchViewModel);

    }
}
