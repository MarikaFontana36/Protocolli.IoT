using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.API.Controllers
{
    [Route("api/Drones/:id")]
    [ApiController]
    public class GetByIdController : ControllerBase
    {
        private readonly IDronesService _dronesService;
        private readonly ILogger<GetByIdController> _logger;

        public GetByIdController(ILogger<GetByIdController> logger, IDronesService dronesService)
        {
            _logger = logger;
            _dronesService = dronesService;
        }

        [HttpGet]
        public string Get(int id)
        {
            var drone = _dronesService.GetByDroneId(id);
            var Json = JsonSerializer.Serialize(drone);
            //drones = ;
            return Json;

        }
    }
}
