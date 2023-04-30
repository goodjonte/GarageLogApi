using GarageLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehcileController : ControllerBase
    {
        private readonly ILogger<VehcileController> _logger;

        public VehcileController(ILogger<VehcileController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetVehciles")]
        public IEnumerable<Vehcile> Get()
        {
            return new Vehcile[]
            {
                new Vehcile
                {
                    Id = Guid.NewGuid(),
                    Name = "Test"
                }
            };
        }
    }
}
