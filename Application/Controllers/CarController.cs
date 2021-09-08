using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.APIModels;
using Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Exceptions;


namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        readonly IMapper _mapper;
        readonly ICarService _carService;
        readonly ILogger<CarController> _logger;


        public CarController(
            IMapper mapper,
            ICarService carService,
            ILogger<CarController> logger
            )
        {
            _mapper = mapper;
            _carService = carService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> RegisterCarForClientAsync(string clientIDNumber, Car car)
        {
            try
            {
                await _carService
                    .RegisterCarForClientAsync(clientIDNumber, _mapper.Map<Domain.DTOs.Car>(car));

            }
            catch (CarAlreadyRegisteredForClientException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest(ex.Message);

            }

            _logger.LogInformation($"successfully added new car (VIN: {car.VIN}) for the client {clientIDNumber} ");

            return Ok(car);
           // return CreatedAtAction(nameof(GetCar), new { id = client.IDNumber }, client);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCarForClientAsync(string clientIDNumber, string carVINCode )
        {
            try
            {
                await _carService.DeleteCarForClientAsync(clientIDNumber, carVINCode);
            }
            catch (InvalidRequestException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }

            _logger.LogInformation($"successfully delete car (VIN: {carVINCode}) for the client {clientIDNumber} ");

            return Ok();

        }
       // [HttpGet]
       // public async Task<ActionResult<Car>> GetCar
    }
}
