using GarageLog.Models;
using GarageLog.Data;
using Microsoft.AspNetCore.Mvc;
using static GarageLog.Models.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenanceController : Controller
    {
        public GarageLogContext _context { get; set; }
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(ILogger<MaintenanceController> logger, GarageLogContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetMaintenance")]
        public async Task<List<Maintenance>> Get()
        {
            
            return await _context.Maintenance.ToListAsync();
        }

        [HttpPost(Name = "CreateMaintenance")]
        public async Task<Maintenance> Post([Bind("Id, VehcileId, Name, DueDate, DueKilometers, DueHours, Notes, MaintType, Email")] Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                maintenance.Id = Guid.NewGuid();
                _context.Add(maintenance);
                await _context.SaveChangesAsync();
                return maintenance;
            }
            return maintenance;
        }
    }
}
