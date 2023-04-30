using GarageLog.Data;
using GarageLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public GarageLogContext _context { get; set; }
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, GarageLogContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Get A User
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _context.User.Where( u => u.Email == email).ToListAsync();
            if (user[0] == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //Create A User
        [HttpPost("{email}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(string email)
        {
            List<User> userList = await _context.User.Where(u => u.Email == email).ToListAsync();
            if (userList.Count < 1)
            {
                User newUser = new User { Id = Guid.NewGuid(), Email = email };
                _context.User.Add(newUser);
                await _context.SaveChangesAsync();
                return Accepted();
            }
            
            return Conflict();
            
        }
    }
}