using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Kinde.Api;
using Kinde.Api.Api;
using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RealesApi.DTA.Intefaces;
using RealesApi.DTO.LoginDTO;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace RealesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuth _auth;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration config,IAuth auth, ILogger<AuthController> logger)
        {
            _config = config;
            _auth = auth;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async  Task<IActionResult>Login([FromBody] UserLoginDTO userLogin)
        {
            var client = new KindeClient(new ApplicationConfiguration("https://realest.kinde.com", "", ""), new KindeHttpClient());
            await client.Authorize(new ClientCredentialsConfiguration("8a0a26a46ccb476d8b32b8439a23bb50", "", "TVyLRqmF1VUBAbAEliecO4lJRM6WlAmriASLBVkJQWxTTM6KjK", "https://realest.kinde.com/api"));

            var usersApi = new UsersApi(client);
            var users = await usersApi.GetUsersAsync();
            foreach (var user2 in users.Users)
            {
                Console.WriteLine($"Id: {user2.Id}, Email: {user2.Email}, Name: {user2.FirstName} {user2.LastName}.");
            }

            var user = AuthenticateUser(userLogin);
            if (user == null)
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpGet("user")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            try
            {
                // Log all claims for debugging
                Console.WriteLine("Claims present: {ClaimCount}", User.Claims.Count());
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine("Claim: {Type} = {Value}", claim.Type, claim.Value);
                }

                // Get the current user from the claims - handle different claim types
                var user = new
                {
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? User.FindFirstValue("sub")
                        ?? User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"),
                    Email = User.FindFirstValue(ClaimTypes.Email)
                        ?? User.FindFirstValue("email"),
                    GivenName = User.FindFirstValue(ClaimTypes.GivenName)
                        ?? User.FindFirstValue("given_name"),
                    FamilyName = User.FindFirstValue(ClaimTypes.Surname)
                        ?? User.FindFirstValue("family_name"),
                    Name = User.FindFirstValue(ClaimTypes.Name)
                        ?? User.FindFirstValue("name")
                };

                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving user info",ex);
                return StatusCode(500, "An error occurred while retrieving user information");
            }
        }
        [HttpGet("claims")]
        [Authorize]
        public IActionResult GetAllClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }
        [HttpGet("token")]
        [Authorize]
        public IActionResult GetToken()
        {
            // The token is available in the Authorization header
            // This is just an example to demonstrate access to the token
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return Ok(new { token = "Token retrieved successfully" });
            // Note: For security reasons, we're not returning the actual token
        }
        // Public endpoint - no authorization required
        [HttpGet("public")]
        public IActionResult GetPublicResource()
        {
            return Ok(new { message = "This is a public resource" });
        }

        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            return Ok(new
            {
                message = "This is a protected endpoint",
                identity = new
                {
                    name = User.Identity?.Name,
                    isAuthenticated = User.Identity?.IsAuthenticated,
                    authenticationType = User.Identity?.AuthenticationType
                },
                claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
            });
        }
        [HttpGet("check-auth-header")]
        public IActionResult CheckAuthHeader()
        {
            // Check if authorization header exists
            if (Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                return Ok(new
                {
                    hasAuthHeader = true,
                    headerValue = authHeader.ToString().Substring(0, Math.Min(20, authHeader.ToString().Length)) + "..."
                });
            }

            return Ok(new { hasAuthHeader = false });
        }
        [HttpPost("decode-token")]
        public IActionResult DecodeToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Token is required");
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(request.Token);

                return Ok(new
                {
                    Header = token.Header,
                    Claims = token.Claims.Select(c => new { c.Type, c.Value }).ToList(),
                    ValidFrom = token.ValidFrom,
                    ValidTo = token.ValidTo,
                    Issuer = token.Issuer,
                    Audiences = token.Audiences
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        public class TokenRequest
        {
            public string Token { get; set; }
        }

        // Example for role-based authorization (if Kinde provides roles)
        [HttpGet("admin")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAdminResource()
        {
            return Ok(new { message = "This is an admin resource" });
        }


        private UserDTO AuthenticateUser(UserLoginDTO userLogin)
        {
            var result = _auth.AuthenticateUser(userLogin);
            return result;

        }

        private string GenerateJwtToken(UserDTO user)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                var credentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = GenerateClaims(user),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = credentials,
                    Audience = _config["Jwt:Audience"],  // Set the Audience here
                    Issuer = _config["Jwt:Issuer"],      // Set the Issuer here
                };

                var token = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Log the exception message
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }
        }


        private static ClaimsIdentity GenerateClaims(UserDTO user)
        {
            var claims = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())

            });
        

            //foreach (var role in user.Roles)
            //    claims.AddClaim(new Claim(ClaimTypes.Role, role));

            return claims;
        }


        [HttpGet("token-info")]
        [Authorize]
        public IActionResult GetTokenInfo()
        {
            try
            {
                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
                _logger.LogInformation("Auth header: {AuthHeader}",
                    authHeader.Length > 20 ? $"{authHeader.Substring(0, 20)}..." : authHeader);

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return BadRequest("No valid authorization header found");
                }

                var token = authHeader.Substring("Bearer ".Length);
                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(token))
                {
                    return BadRequest("Invalid token format");
                }

                var jwtToken = handler.ReadJwtToken(token);

                // Log and extract info without validation
                var claims = jwtToken.Claims.Select(c => new { c.Type, c.Value }).ToList();

                return Ok(new
                {
                    Issuer = jwtToken.Issuer,
                    ValidFrom = jwtToken.ValidFrom,
                    ValidTo = jwtToken.ValidTo,
                    Claims = claims,
                    HeaderData = jwtToken.Header.ToDictionary(h => h.Key, h => h.Value?.ToString())
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing token");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

