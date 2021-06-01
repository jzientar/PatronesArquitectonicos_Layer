using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealerAPI.Exceptions;
using DealerAPI.Models;
using DealerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DealerAPI.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class DealersController : ControllerBase
    {
        private IDealerService service;

        public DealersController(IDealerService service)
        {
            this.service = service;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<DealerModel>> GetDealers(string orderBy = "id")
        {
            try
            {
                return Ok(service.GetDealers(orderBy));
            }
            catch (BadOperationRequest ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<DealerModel> GetDealer(int id)
        {
            try
            {
                return Ok(service.GetDealer(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<DealerModel> CreateDealer([FromBody] DealerModel dealer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newDealer = service.CreateDealer(dealer);
                return Created($"/api/Dealers/{newDealer.Id}", newDealer);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteDealer(int id)
        {
            try
            {
                var dlr = service.DeleteDealer(id);
                return Ok(dlr);
            }
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpPut("{id:int}")]
        public ActionResult<bool> UpdateDealer(int id, [FromBody] DealerModel dealer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var state in ModelState)
                    {
                        if (state.Key == nameof(dealer.Address) && state.Value.Errors.Count > 0)
                        {
                            return BadRequest(state.Value.Errors);
                        }
                    }
                }

                return Ok(service.UpdateDealer(id, dealer));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
