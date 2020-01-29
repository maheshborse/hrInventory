using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HRInventories.Services.Interface;

namespace SynerzipInterviewApp.Controllers
{
    [Route("api/[controller]")]
    public class DirectoryController : BaseController
    {
        private readonly IAuthenticationRepository authService;
        public DirectoryController(IAuthenticationRepository authService)
        {
            this.authService = authService;
        }

        [HttpGet("GetUsers")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers()
        {
            try
            {
                var users = authService.GetUsers();
                if (users == null)
                {
                    return NotFound();
                }

                var ActiveUsers = new { ActiveUsers = users };
                return Ok(JsonConvert.SerializeObject(ActiveUsers));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
          
        }
    }
}