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
    public class RequestController : ControllerBase
    {
        IRequestDataAccess _iRequestDataAccess;
        public RequestController(IRequestDataAccess requestDataAccess)
        {
            _iRequestDataAccess = requestDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertRequest([FromBody]Request req)
        {
            try
            {
                await _iRequestDataAccess.InsertRequest(req);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            try
            {
                List<Request> request = await _iRequestDataAccess.GetRequests();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpGet("{id}", Name = "GetRequest")]
        public IActionResult GetRequest(long id)
        {
            try
            {
                Request request = _iRequestDataAccess.GetRequestbyID(id);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRequests(long id, [FromBody]Request req)
        {
            try
            {
                Request requestToUpdate = _iRequestDataAccess.UpdateRequests(req);
                return Ok();
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteRequests(long id)
        {
            _iRequestDataAccess.DeleteRequests(id);
            return Ok();
        }
    }
}