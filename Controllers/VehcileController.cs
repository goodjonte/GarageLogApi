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
        //FIELDS and PROPERTIES
        private Vehcile vehcile1= new Vehcile();
        public readonly IConfiguration _configuration;
        public GarageLogContext _context { get; set; }

        //CONSTRUCTOR
        public VehcileController(IConfiguration configuration, GarageLogContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        //METHODS

        //api/Vehcile GET
        //Get Users Vehciles
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

        //api/Vehcile POST
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

        //api/Vehcile/{vehcileId} GET
        //Get A Singular Vehcile
        [HttpGet("{vehcileId}")]
        public async Task<IActionResult> GetAVehcile(Guid vehcileId)
        {
            var vehcile = await _context.Vehcile.FindAsync(vehcileId);
            if (vehcile != null)
            {
                return Ok(vehcile);
            }
            if (vehcile == null)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        //api/Vehcile/{id} DELETE
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

        //api/Vehcile/ POST
        //Update A Vehciles Mileage/Kilometers
        [HttpPost("EditMileage")]
        public async Task<IActionResult> UpdateVehcile(MileageDTO mileageObj)
        {
            
            if (mileageObj != null)
            {
                var vehcile = await _context.Vehcile.FindAsync(mileageObj.VehcileId);
            
            if (vehcile != null)
            {
                vehcile.KilometersOrHours = mileageObj.Mileage;
                _context.Vehcile.Update(vehcile);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            }
            return NotFound();
           
        }

        //Creates a TokenValidationParameters instance to use to validate the JWT token
        private TokenValidationParameters GetValidationParameters()
        {
            var key = _configuration.GetSection("AppSettings:Token").Value;

            if (key != null)
            {
                return new TokenValidationParameters()
                {
                    ValidateLifetime = false,
                    ValidateAudience = false, 
                    ValidIssuer = "GarageLog",
                    ValidAudience = "GarageLog",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) 
                };
            }
            else
            {
                throw (new NoKeyInAppSettings("No Key Found In App Settings"));
            }
        }
    }
}