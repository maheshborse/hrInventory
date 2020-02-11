using HRInventories.Models;
using HRInventories.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IRequestDataAccess
    {
        Task AddRequest(RequestViewModel requestViewModel);

        Task<List<ReqestMasterModel>> GetReqest();

        Task UpdateReqest(RequestViewModel requestViewModel);

        Task DeleteReqest(int requestid);
    }
}
