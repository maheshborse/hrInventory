using HRInventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
    public interface IPomasterDataAccess
    {
        Task AddPomaster(Pomaster pomaster);

        Task<List<Pomaster>> GetPomasters();

        Pomaster GetPomasterbyID(long id);

        Pomaster UpdatePomaster(Pomaster item);

        void DeletePomaster(Pomaster pomaster);
    }
}
