using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Controllers
{
    // ✅ This controller handles authentication (login) for users.
    [Route("api/auth")] // The base URL for this controller will be: http://localhost:5169/api/auth
    [ApiController] // Marks this as an API controller (it automatically handles HTTP request validation).
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config; // ✅ This is used to read settings from appsettings.json (like JWT secret key).

        // ✅ Constructor: This is called when the AuthController is created.
        public AuthController(IConfiguration config)
        {
            _config = config; // Stores the configuration settings for later use.
        }

        // ✅ HTTP POST method to handle user login.
        [HttpPost("login")] // The endpoint will be: http://localhost:5169/api/auth/login
        public IActionResult Login([FromBody] LoginRequest request) // Takes a JSON request body with username & password.
        {
            // ✅ Simple hardcoded authentication check (for testing purposes).
            if (request.Username == "admin" && request.Password == "admin123")
            {
                var token = GenerateJwtToken(UserRoles.Admin); // Generate a JWT token for the Admin user.
                return Ok(new { Token = token }); // Return the token in JSON format.
            }
            else if (request.Username == "user" && request.Password == "user123")
            {
                var token = GenerateJwtToken(UserRoles.User); // Generate a JWT token for the regular User.
                return Ok(new { Token = token }); // Return the token in JSON format.
            }

            // ✅ If username/password is incorrect, return "Unauthorized" response.
            return Unauthorized("Invalid username or password");
        }

        // ✅ Method to generate a JWT token (this is called inside the Login method above).
        private string GenerateJwtToken(UserRoles role)
        {
            // ✅ Step 1: Create a security key using a secret key from appsettings.json.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // ✅ Use HMAC-SHA256 for token security.

            // ✅ Step 2: Define the claims (extra data stored inside the token).
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, role.ToString()), // ✅ Store the user's role (Admin/User) inside the token.
                new Claim(JwtRegisteredClaimNames.Aud, _config["Jwt:Audience"]) // ✅ Set the audience claim (who the token is for).
            };

            // ✅ Step 3: Create the JWT token.
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"], // ✅ Set the token issuer (who issued it).
                audience: _config["Jwt:Audience"], // ✅ Ensure the audience matches what's expected.
                claims: claims, // ✅ Attach claims (role, audience, etc.).
                expires: DateTime.UtcNow.AddHours(1), // ✅ Set token expiration time (valid for 1 hour).
                signingCredentials: credentials // ✅ Sign the token with the security key.
            );

            // ✅ Step 4: Convert the JWT token object into a string and return it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // ✅ A simple class to store login request data (sent by the user in JSON format).
    public class LoginRequest
    {
        public string Username { get; set; } // ✅ Stores the username entered by the user.
        public string Password { get; set; } // ✅ Stores the password entered by the user.
    }
}
