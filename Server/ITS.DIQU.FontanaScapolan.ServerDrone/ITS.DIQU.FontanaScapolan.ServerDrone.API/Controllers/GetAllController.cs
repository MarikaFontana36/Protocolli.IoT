using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.API.Controllers
{
    [ApiController]
    [Route("api/Drones")]
    public class GetAllController : ControllerBase
    {
        private readonly IDronesService _dronesService;
        private readonly ILogger<GetAllController> _logger;

        public GetAllController(ILogger<GetAllController> logger, IDronesService dronesService)
        {
            _logger = logger;
            _dronesService = dronesService;
        }

        [HttpGet]
        public string Get()
        {
            var drones = _dronesService.GetAllDrones();
            var Json = JsonSerializer.Serialize(drones);
            return Json;

        }
    }
}
