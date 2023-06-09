﻿using GarageLog.Models;
using GarageLog.Data;
using Microsoft.AspNetCore.Mvc;
using static GarageLog.Models.Maintenance;
using Microsoft.EntityFrameworkCore;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : Controller
    {
        //FIELDS and PROPERTIES
        public GarageLogContext _context { get; set; }
        private readonly ILogger<MaintenanceController> _logger;

        //CONSTRUCTOR
        public MaintenanceController(ILogger<MaintenanceController> logger, GarageLogContext context)
        {
            _logger = logger;
            _context = context;
        }

        //METHODS
        //api/Maintenance/{vehcileId} GET
        //Get a Maintenance item
        [HttpGet("{vehcileId}")]
        [ProducesResponseType(typeof(List<Maintenance>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMaintenances(Guid vehcileId)
        {
            List<Maintenance> maintenances = await _context.Maintenance.Where(m => m.VehcileId == vehcileId).ToListAsync();
            if (maintenances.Count < 1)
            {
                return NotFound();
            }
            return Ok(maintenances);
        }

        //api/Maintenance POST
        //Create a Maintenance item
        [HttpPost(Name = "CreateMaintenance")]
        public async Task<string> CreateMaintenance(Maintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                maintenance.Id = Guid.NewGuid();
                _context.Add(maintenance);
                await _context.SaveChangesAsync();
                return "Maintenenace Log Created";
            }
            return "Model State Not Valid!";
        }

        //api/Maintenance/{id} DELETE
        //Delete a Maintenance item
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(Guid id)
        {
            if (_context.Maintenance == null)
            {
                return NotFound();
            }
            var maintenance = await _context.Maintenance.FindAsync(id);
            if (maintenance != null)
            {
                _context.Maintenance.Remove(maintenance);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
