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
            //riceve la richiesta di get all e la passa al servizio dedicato
            var drones = _dronesService.GetAllDrones();
            //serializza in json i dati che riceve dal servizio
            var Json = JsonSerializer.Serialize(drones);
            //restituisce una risposta al client con i dati richiesti
            return Json;

        }
    }
}
