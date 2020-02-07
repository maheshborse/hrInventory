using HRInventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IRequestDataAccess
    {
        Task InsertRequest(Request request);

        Task<List<Request>> GetRequests();
        Request GetRequestbyID(long id);
        Request UpdateRequests(Request item);

        void DeleteRequests(long id);
    }
}
