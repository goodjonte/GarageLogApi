using GarageLog.Data;
using GarageLog.DTO;
using GarageLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GarageLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        public readonly IConfiguration _configuration;
        public GarageLogContext _context { get; set; }

        public UserController(IConfiguration configuration, GarageLogContext context)
        {
            _configuration = configuration;
            _context = context;
        }

 
        
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            if (request.Username == "" || request.Password == "" || request.Username == null || request.Password == null)//Checks if the username or password is null
            {
                return BadRequest("Please enter both a Username and Password!");
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);//Takes in the password and creates the hash and salt

            //Set all the user properties (would push to db but this is example project)
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Id = Guid.NewGuid();

            if (_context.User.Any(x => x.Username == user.Username))//Checks if the username is already taken
            {
                return BadRequest("Username already taken!");
            }

            _context.User.Add(user);//Adds the user to the db context
            await _context.SaveChangesAsync();//Saves the changes to the db

            return Ok(user);

        }

        //Login Post
        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO request)
        {
            bool UserExists = _context.User.Any(x => x.Username == request.Username);
            if (UserExists == false)//Checks if username is valid
            {
                return BadRequest("User not found!");
            }

            var userLoggingIn = _context.User.FirstOrDefault(x => x.Username == request.Username);//Gets the user from the db
            if (userLoggingIn != null)
            {
                if (!VerifyPasswordHash(request.Password, userLoggingIn.PasswordHash, userLoggingIn.PasswordSalt))//checks if password is valid(hash and salt would come from db in real project(the users row))
                {
                    return BadRequest("Wrong Password");
                }
            }
            string token = CreateToken(request);

            return Ok(token);
        }

        //Creates the JWT token Based off the user
        private string CreateToken(UserDTO request)
        {
            List<Claim> claims = new List<Claim>//Creates claims to assign to the jwt token 
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            var keyToken = _configuration.GetSection("AppSettings:Token").Value;
            if (keyToken != null)
            {
                //Creates the key from the token created in appsettings (also turns it into a byte array first)
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyToken));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //creats the creds (pretty much just signature to the key)

                var token = new JwtSecurityToken( //Initialisez the JWT token (putting everything together)
                    claims: claims,
                    issuer: "GarageLog",
                    audience: "GarageLog",
                    expires: DateTime.Now.AddDays(1),//Here is the token expiry (Currently just adding 24hours from creation)
                    signingCredentials: cred
                    );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token); //Actualty writes the token now it can be passed to the client

                return jwt;
            }
            return "No KeyToken in Appsettings";

        }


        //creates password hash and salt sets the out params to the created hash and salt (would need to return in actual example and add to db context)
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Creates a Hash from the entered password then compares it to the existing hash (Would be from db in real example)
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}