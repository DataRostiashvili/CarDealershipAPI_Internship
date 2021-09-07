using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using AutoMapper;
using Domain.APIModels;
namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IClientService clientService, IMapper mapper) 
        {
            _clientService = clientService;
            _mapper = mapper; 
        }

        [HttpPost]
        public async Task<ActionResult<Client>> RegisterClientAsync(Client client)
        {

            await _clientService.InsertClientAsync(_mapper.Map<Domain.DTOs.Client>(client));

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

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClientAsync(Client client) 
        {
            if (_clientService.GetClient(client.IDNumber) is null)
                return BadRequest();

            await  _clientService.UpdateClientAsync(_mapper.Map<Domain.DTOs.Client>(client));

            return Ok();

        }

    }
}
