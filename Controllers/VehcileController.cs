using GarageLog.Data;
using GarageLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehcileController : ControllerBase
    {
        public GarageLogContext _context { get; set; }
        private readonly ILogger<VehcileController> _logger;

        public VehcileController(ILogger<VehcileController> logger, GarageLogContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetVehciles")]
        public async Task<List<Vehcile>> Get()
        {

            return await _context.Vehcile.ToListAsync();
        }
    }
}
