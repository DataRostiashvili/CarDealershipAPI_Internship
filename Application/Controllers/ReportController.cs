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
using Application.Logger;
using Domain.Exceptions;

namespace Application.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly ILoggerAdapter<ClientController> _logger;

        public ReportController(IReportService reportService,
            IMapper mapper,
            ILoggerAdapter<ClientController> logger)
        {
            _reportService = reportService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async  Task<ActionResult<ReportResponse>> GetReport()
        {
            var res = await _reportService.GetReport();

            return Ok(res);
        }

    }
}
