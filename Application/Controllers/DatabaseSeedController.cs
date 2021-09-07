using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.DatabaseSeedLoader;
using Domain.Exceptions;

namespace Application.Controllers
{
    [ApiController]
    [Route("SeedDatabase")]
    public class DatabaseSeedController : ControllerBase
    {
        readonly IDatabaseSeedLoader _databaseSeedLoader;
        public DatabaseSeedController(IDatabaseSeedLoader databaseSeedLoader)
        {
            _databaseSeedLoader = databaseSeedLoader;
        }

#if DEBUG
        [HttpPost]
        public async Task<ActionResult> Index() 
        {
            try
            {
                await _databaseSeedLoader.SeedData();
                return Ok();
            }
            catch (DatabaseAlreadySeededException e)
            {
                return BadRequest(e.Message);
            }
        }
#endif
    }
}
