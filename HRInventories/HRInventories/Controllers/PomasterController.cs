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
        //[HttpPost]
        //public async Task<IActionResult> InsertPo([FromBody]PodetailModel podetail)
        //{
        //    try
        //    {
        //        await _iPomasterDataAccess.AddPo(podetail);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
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
        //[HttpGet]
        //public async Task<IActionResult> GetCategories()
        //{
        //    try
        //    {
        //        List<POViewModel> pomasters = await _iPomasterDataAccess.GetPomasters();
        //        return Ok(pomasters);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //}

        //[HttpGet("{id}", Name = "GetPomaster")]
        //public IActionResult GetPomaster(long id)
        //{
        //    try
        //    {
        //        Pomaster pomaster = _iPomasterDataAccess.GetPomasterbyID(id);
        //        return Ok(pomaster);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //[HttpPut("{id}")]
        //public IActionResult UpdateCategory(long id, [FromBody] Pomaster pomaster)
        //{
        //    try
        //    {
        //        Pomaster pomasterToUpdate = _iPomasterDataAccess.UpdatePomaster(pomaster);
        //        // _iDataAccess.UpdateCatagory(catagoryToUpdate, categories);
        //        return NoContent();
        //    }

        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteCategory(long id)
        //{
        //    Pomaster catagory = _iPomasterDataAccess.GetPomasterbyID(id);
        //    _iPomasterDataAccess.DeletePomaster(catagory);
        //    return NoContent();
        //}
    }
}