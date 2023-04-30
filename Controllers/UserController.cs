using GarageLog.Data;
using GarageLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public GarageLogContext _context { get; set; }
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, GarageLogContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<List<User>> Get()
        {

            return await _context.User.ToListAsync();
        }
    }
}