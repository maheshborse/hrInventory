using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryDataAccess.Models;
using InventoryDataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HRMSInventoryMangement.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsPolicy")]
    //[ApiController]
    public class CategoryController : Controller
    {
        IDataAccess _iDataAccess;
        public CategoryController(IDataAccess iDataAccess)
        {
            _iDataAccess = iDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody]Category categories)
        {
            await _iDataAccess.AddCategory(categories);
            return Ok("Success");
        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var getcatogory = _context.categories.ToList();
        //    return Ok(getcatogory);
        //}

    }
}