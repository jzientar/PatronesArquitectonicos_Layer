using DealerAPI.Exceptions;
using DealerAPI.Models;
using DealerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerAPI.Controllers
{
    [Route("api/dealers/{dealerId:int}/[controller]")]
    public class CarsController : Controller
    {
        private ICarService service;
        public CarsController(ICarService service)
        {
            this.service = service;
        }
        [HttpDelete("{id:int}")]
        public ActionResult<bool> DeleteCar(int dealerId, int id)
        {
            try
            {
                return Ok(service.DeleteCar(dealerId, id));
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

        [HttpPut("{id:int}")]
        public ActionResult<bool> UpdateCar(int dealerId, int id, [FromBody]CarModel car)
        {
            try
            {
                return Ok(service.UpdateCar(dealerId, id, car));
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
        [HttpGet("{id:int}")]
        public ActionResult<CarModel> GetCar(int dealerId, int id)
        {
            try
            {
                return Ok(service.GetCar(dealerId, id));
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
        public ActionResult<CarModel> CreateCar(int dealerId, [FromBody]CarModel car)
        {
            try
            {
                var newCar = service.CreateCar(dealerId, car);
                return Created($"api/dealers/{dealerId}/cars/{newCar.Id}", newCar);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult<CarModel> GetCars(int dealerId)
        {
            try
            {
                return Ok(service.GetCars(dealerId));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
