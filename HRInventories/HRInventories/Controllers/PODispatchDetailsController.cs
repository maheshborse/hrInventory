using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInventories.Models;
using HRInventories.Services;
using HRInventories.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRInventories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PODispatchDetailsController : ControllerBase
    {
        IPODispatchDetailsDataAccess _iPODispatchDetailsDataAccess;
        public PODispatchDetailsController(IPODispatchDetailsDataAccess pODispatchDetailsDataAccess)
        {
            _iPODispatchDetailsDataAccess = pODispatchDetailsDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> GetPODispatchDetails(int id)
        {
            try
            {
                List<PODispatchDetailsGrid> pODispatchDetails = await _iPODispatchDetailsDataAccess.GetPODispatchDetails(id);
                return Ok(pODispatchDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}