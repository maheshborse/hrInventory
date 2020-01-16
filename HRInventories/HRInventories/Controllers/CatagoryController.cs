using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInventories.Models;
using HRInventories.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRInventories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        IDataAccess _iDataAccess;
        public CatagoryController(IDataAccess dataAccess)
        {
            _iDataAccess = dataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody]Catagory categories)
        {
            try
            {
                await _iDataAccess.AddCategory(categories);
                return Ok("Successfully Inserted");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                List<Catagory> Categories = await _iDataAccess.GetCategories();
                return Ok(Categories);
            }
                catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        
        }

        //public IActionResult Delete(long id)
        //{
        //    Catagory Catagories = _iDataAccess.Get(id);
        //    if (interview == null)
        //    {
        //        return NotFound("The Interview record couldn't be found.");
        //    }

        //    _dataRepository.Delete(interview);
        //    return NoContent();
        //}
    }
}