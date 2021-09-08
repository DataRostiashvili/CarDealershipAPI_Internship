using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using AutoMapper;
using Domain.APIModels;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService,
            IMapper mapper,
            ILogger<ClientController> logger) 
        {
            _clientService = clientService;
            _mapper = mapper; 
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Client>> RegisterClientAsync(Client client)
        {

            await _clientService.InsertClientAsync(_mapper.Map<Domain.DTOs.Client>(client));

            _logger.LogInformation($"client with idNumber {client.IDNumber} registered successfully");

            return CreatedAtAction(nameof(GetClient), new { id = client.IDNumber }, client);
        }

        [HttpGet]
        public ActionResult<Client> GetClient(string idNumber) 
        {
            var dtoClient = _clientService.GetClient(idNumber);
            return Ok(_mapper.Map<Domain.APIModels.Client>(dtoClient));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteClientAsync(string idNumber) 
        {
            await _clientService.DeleteClientAsync(idNumber);

            _logger.LogInformation($"client with idNumber {idNumber} has been deleted");

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClientAsync(Client client) 
        {
            if (_clientService.GetClient(client.IDNumber) is null)
            {
                _logger.LogInformation($"client with idNumber {client.IDNumber} doesn't exists");
                return BadRequest();
            }

            await  _clientService.UpdateClientAsync(_mapper.Map<Domain.DTOs.Client>(client));

            _logger.LogInformation($"client with idNumber {client.IDNumber} has been updated successfully");


            return Ok();

        }

    }
}
