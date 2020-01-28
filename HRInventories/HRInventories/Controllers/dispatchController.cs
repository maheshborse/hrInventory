using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInventories.Models;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRInventories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dispatchController : ControllerBase
    {
        IdispatchDataAccess _idispatchDataAccess;
        public dispatchController(IdispatchDataAccess dispatchDataAccess)
        {
            _idispatchDataAccess = dispatchDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> Insertdispatch([FromBody]DispatchViewModel dispatchViewModel)
        {
            try
            {
                await _idispatchDataAccess.Adddispatch(dispatchViewModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Getdispatch()
        {
            try
            {
                List<DispatchmasterModel> pomasters = await _idispatchDataAccess.Getdispatch();
                return Ok(pomasters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateDispatch([FromBody]DispatchViewModel dispatchViewModel)
        {
            try
            {
                _idispatchDataAccess.UpdateDispatch(dispatchViewModel);
                return NoContent();
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}