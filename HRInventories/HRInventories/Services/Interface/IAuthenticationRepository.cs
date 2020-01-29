using HRInventories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRInventories.Services.Interface
{
   public  interface IAuthenticationRepository
    {
        User Login(Login login);
        List<User> GetUsers();
    }
}
