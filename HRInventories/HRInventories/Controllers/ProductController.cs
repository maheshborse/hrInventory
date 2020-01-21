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
    public class ProductController : ControllerBase
    {
        IDataAccess _iDataAccess;
        public ProductController(IDataAccess dataAccess)
        {
            _iDataAccess = dataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody]Product products)
        {
            try
            {
                await _iDataAccess.AddProduct(products);
                return Ok("Successfully Inserted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                List<Product> Products = await _iDataAccess.GetProducts();
                return Ok(Products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(long id)
        {
            try
            {
                Product product = _iDataAccess.GetProductbyID(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(long id, [FromBody] Product products)
        {
            try
            {
                Product productToUpdate = _iDataAccess.GetProductbyID(id);
                _iDataAccess.UpdateProduct(productToUpdate, products);
                return Ok("Successfully updated");
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(long id)
        {
            Product product = _iDataAccess.GetProductbyID(id);
            _iDataAccess.DeleteProduct(product);
            return Ok("deleted");
        }
    }
}