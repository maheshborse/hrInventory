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
    public class RequestController : ControllerBase
    {
        IRequestDataAccess _iRequestDataAccess;
        public RequestController(IRequestDataAccess requestDataAccess)
        {
            _iRequestDataAccess = requestDataAccess;
        }
        [HttpPost]
        public async Task<IActionResult> InsertRequest([FromBody]RequestViewModel requestdetail)
        {
            try
            {
                await _iRequestDataAccess.AddRequest(requestdetail);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRequest()
        {
            try
            {
                List<ReqestMasterModel> Reqestmasters = await _iRequestDataAccess.GetReqest();
                return Ok(Reqestmasters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateReqest([FromBody] RequestViewModel requestViewModel)
        {
            try
            {
                _iRequestDataAccess.UpdateReqest(requestViewModel);
                return NoContent();
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{requestid}")]
        public IActionResult DeleteReqest(int requestid)
        {

            _iRequestDataAccess.DeleteReqest(requestid);
            return NoContent();
        }
    }
}