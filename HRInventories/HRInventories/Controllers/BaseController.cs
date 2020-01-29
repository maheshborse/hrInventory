using HRInventories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SynerzipInterviewApp.Controllers
{
    public class BaseController: Controller
    {
        public JWTUserModel JWTUser = new JWTUserModel();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            JWTUser = JWTSettings.GetJWTUser(HttpContext?.User?.Claims);
        }
    }


  
}