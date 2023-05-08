using GarageLog.Data;
using GarageLog.Models;
using GarageLog.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Principal;
using GarageLog.ErrorHandling;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehcileController : ControllerBase
    {
        private Vehcile vehcile1= new Vehcile();
        public readonly IConfiguration _configuration;
        public GarageLogContext _context { get; set; }
        public VehcileController(IConfiguration configuration, GarageLogContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        //Get A Vehcile
        [HttpGet(Name = "GetVehciles")]
        [ProducesResponseType(typeof(List<Vehcile>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetVehciles(string JWT)
        {
            if (JWT != null)
            {
                //This will Throw an error if JWT is not valid
                SecurityToken validatedToken;
                IPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(JWT, GetValidationParameters(), out validatedToken);

                JwtSecurityToken? jwt = new JwtSecurityTokenHandler().ReadToken(JWT) as JwtSecurityToken;

                if (jwt != null)
                {
                    string UserNameFromJWT = jwt.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                   
                    User? thisUser = _context.User.FirstOrDefault(u => u.Username == UserNameFromJWT);
                    if (thisUser != null)
                    {
                        Guid userId = thisUser.Id;
                        List<Vehcile> vehciles = await _context.Vehcile.Where(v => v.UserID == userId).ToListAsync();
                        if (vehciles.Count < 1)
                        {
                            return NotFound();
                        }
                        return Ok(vehciles);
                    }
                }
            }
            return NotFound();
        }

        //Create A Vehcile
        [HttpPost(Name = "CreateVehcile")]
        public async Task<string> CreateVehcile(VehcileDTO newVehcile)
        {
            if (ModelState.IsValid)
            {
                vehcile1.Id = Guid.NewGuid();
                vehcile1.Name = newVehcile.Name;
                vehcile1.Img = newVehcile.Img;
                vehcile1.IsHours = newVehcile.IsHours;
                vehcile1.KilometersOrHours = newVehcile.KilometersOrHours;
                vehcile1.VehcileType = newVehcile.VehcileType;

                //This will Throw an error if JWT is not valid
                SecurityToken validatedToken;
                IPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(newVehcile.JWT, GetValidationParameters(), out validatedToken);

                JwtSecurityToken? jwt = new JwtSecurityTokenHandler().ReadToken(newVehcile.JWT) as JwtSecurityToken;

                if(jwt != null)
                {
                    string UserNameFromJWT = jwt.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;

                    User? thisUser = _context.User.FirstOrDefault(u => u.Username == UserNameFromJWT);
                    if(thisUser != null)
                    {
                        vehcile1.UserID = thisUser.Id;


                        _context.Add(vehcile1);
                        await _context.SaveChangesAsync();
                        return "Vehcile Created";
                    }
                }
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

        private TokenValidationParameters GetValidationParameters()
        {
            var key = _configuration.GetSection("AppSettings:Token").Value;

            if (key != null)
            {
                return new TokenValidationParameters()
                {
                    ValidateLifetime = false,
                    ValidateAudience = false, // Because there is no audiance in the generated token
                    ValidateIssuer = false,   // Because there is no issuer in the generated token
                    ValidIssuer = "GarageLog",
                    ValidAudience = "GarageLog",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) // The same key as the one that generate the token
                };
            }
            else
            {
                throw (new NoKeyInAppSettings("No Key Found In App Settings"));
            }
        }
    }
}