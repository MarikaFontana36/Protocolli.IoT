using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DronesController : ControllerBase
    {
        private readonly IDronesService _dronesService;
        private readonly ILogger<DronesController> _logger;

        public DronesController(ILogger<DronesController> logger, IDronesService dronesService)
        {
            _logger = logger;
            _dronesService = dronesService;
        }

        [HttpPost]
        public string Post(Drone drone)
        {
            //diceve i dati dalla chiamata post e li passa al servizio di inserimento
            _dronesService.Insert(drone);
            //manda una risposta al client passando i dati inseriti come conferma
            return JsonSerializer.Serialize(drone).ToString();

        }
    }
}
