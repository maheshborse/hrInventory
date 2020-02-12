using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRInventories.Models;
using HRInventories.Services.Interface;
using HRInventories.UIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HRInventories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductDataAccess _iProductDataAccess;
        public ProductController(IProductDataAccess productDataAccess)
        {
            _iProductDataAccess = productDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody]ProductModel products)
        {
            try
            {
                await _iProductDataAccess.AddProduct(products);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, ex.Message);
                return StatusCode(409);
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
                List<ProductModel> Products = await _iProductDataAccess.GetProducts();
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
                Product product = _iProductDataAccess.GetProductbyID(id);
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

                Product productsToUpdate = _iProductDataAccess.UpdateProduct(products);
                return Ok();
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(long id)
        {
            //Product product = _iProductDataAccess.GetProductbyID(id);
            _iProductDataAccess.DeleteProduct(id);
            return Ok();
        }
    }
}