using GarageLog.Models;
using Microsoft.AspNetCore.Mvc;
using static GarageLog.Models.Maintenance;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenanceController : Controller
    {
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(ILogger<MaintenanceController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMaint")]
        public IEnumerable<Maintenance> Get()
        {
            return new Maintenance[]
            {
                new Maintenance
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    MaintType = MType.AirFilter
                }
            };
        }
    }
}
