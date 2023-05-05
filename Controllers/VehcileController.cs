﻿using GarageLog.Data;
using GarageLog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehcileController : ControllerBase
    {
        public GarageLogContext _context { get; set; }
        private readonly ILogger<VehcileController> _logger;

        public VehcileController(ILogger<VehcileController> logger, GarageLogContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Get A Vehcile
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(List<Vehcile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehciles(Guid userId)
        {

            List<Vehcile> vehciles = await _context.Vehcile.Where(v => v.UserID == userId).ToListAsync();
            if (vehciles.Count < 1)
            {
                return NotFound();
            }
            return Ok(vehciles);
        }

        //Create A Vehcile
        [HttpPost(Name = "CreateVehcile")]
        public async Task<string> CreateVehcile([Bind("Id, UserID, Name, Img, IsHours, KilometersOrHours, VehcileType")] Vehcile vehcile)
        {
            if (ModelState.IsValid)
            {
                vehcile.Id = Guid.NewGuid();
                _context.Add(vehcile);
                await _context.SaveChangesAsync();
                return "Success";
            }
            return "ModelState Not Valid";
        }

        //Delete A Vehcile
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehcile(Guid id)
        {
            var vehcile = await _context.Vehcile.FindAsync(id);
            if (vehcile != null)
            {
                _context.Vehcile.Remove(vehcile);
            }
            if (vehcile == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Edit a Maintenance item
        //[HttpPost(Name = "EditMaintenance")]
        //public async Task<Maintenance> EditMaintenance()
        //{

        //}
    }
}
