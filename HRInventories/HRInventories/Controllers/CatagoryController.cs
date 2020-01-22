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
    public class CatagoryController : ControllerBase
    {
        ICatagoryDataAccess _iCatagoryDataAccess;
        public CatagoryController(ICatagoryDataAccess catagoryDataAccess)
        {
            _iCatagoryDataAccess = catagoryDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody]CatagoryModel categories)
        {
            try
            {
                await _iCatagoryDataAccess.AddCategory(categories);
                return NoContent();
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
                List<Catagory> Categories = await _iCatagoryDataAccess.GetCategories();
                return Ok(Categories);
            }
                catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        
        }

        [HttpGet("{id}", Name = "GetCatagory")]
        public IActionResult GetCatagory(long id)
        {
            try
            {
                Catagory catagory = _iCatagoryDataAccess.GetCatagorybyID(id);
                return Ok(catagory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(long id, [FromBody]CatagoryModel categories)
        {
            try
            {
                Catagory catagoryToUpdate = _iCatagoryDataAccess.UpdateCatagory(categories);
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
            Catagory catagory = _iCatagoryDataAccess.GetCatagorybyID(id);
            _iCatagoryDataAccess.DeleteCatagory(catagory);
            return NoContent();
        }
    }
}