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
    public class PomasterController : ControllerBase
    {
        IPomasterDataAccess _iPomasterDataAccess;
        public PomasterController(IPomasterDataAccess pomasterDataAccess)
        {
            _iPomasterDataAccess = pomasterDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertPo([FromBody]POViewModel podetail)
        {
            try
            {
                await _iPomasterDataAccess.AddPo(podetail);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPo()
        {
            try
            {
                List<PomasterModel> pomasters = await _iPomasterDataAccess.GetPo();
                return Ok(pomasters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        
        [HttpPut("{id}")]
        public IActionResult UpdatePo([FromBody] POViewModel pOViewModel)
        {
            try
            {
               _iPomasterDataAccess.UpdatePo(pOViewModel);
                return NoContent();
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //[HttpDelete("{id}")]
        //public IActionResult DeletePo(long id)
        //{
           
        //    _iPomasterDataAccess.DeletePo(pomaster);
        //    return NoContent();
        //}
    }
}