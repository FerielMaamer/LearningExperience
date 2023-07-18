using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTracker.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTracker.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly TaskDbContext _context;
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userService;

        public UserController(TaskDbContext context, IConfiguration configuration, IUserServices userService)
        {
            _context = context;
            _configuration = configuration;
            _userService = userService;
        }
        //Authentication
        [HttpPost("signin")]
        public IActionResult SignIn(UserCredentials credentials)
        {
            // Perform sign-in logic
            // Validate user credentials and generate authentication token

            var user = _context.User.FirstOrDefault(e => e.Email == credentials.Email && e.Password == credentials.Password);

            if (user != null)
            {
                var userId = user.UserId.ToString(); // Convert the UserId to a string
                var jwtToken = GenerateJwtToken(userId, user.FirstName, 30);
                return Ok(new { token = jwtToken });
            }
            else
            {
                return Unauthorized();
            }
        }




        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp(User user)
        {
            // Perform sign-up logic
            // Create new user, store in database, etc.
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var userId = user.UserId.ToString(); // Convert the UserId to a string
            var jwtToken = GenerateJwtToken(userId, user.FirstName, 30);
            Console.WriteLine(user);
            return Ok(new { token = jwtToken });
            //return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            // Return appropriate response
            //return Ok();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
        private string GenerateJwtToken(string userId, string fname, int expirationMinutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, fname)
        }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        ////User Library
        //// GET: api/users
        [HttpGet]

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return Ok(users);

        }

        //// GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        //// PUT: api/users/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, User user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/users/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.User.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.User.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


    }

}
public class UserCredentials
{
    public string Email { get; set; }
    public string Password { get; set; }
}
