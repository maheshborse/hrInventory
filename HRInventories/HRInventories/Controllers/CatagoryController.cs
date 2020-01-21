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

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            try
            {
                Catagory catagory = _iDataAccess.GetCatagorybyID(id);
                return Ok(catagory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(long id, [FromBody] Catagory categories)
        {
            try
            {
                Catagory catagoryToUpdate = _iDataAccess.UpdateCatagory(categories);
               // _iDataAccess.UpdateCatagory(catagoryToUpdate, categories);
                return NoContent();
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(long id)
        {
            Catagory catagory = _iDataAccess.GetCatagorybyID(id);
            _iDataAccess.DeleteCatagory(catagory);
            return NoContent();
        }
    }
}