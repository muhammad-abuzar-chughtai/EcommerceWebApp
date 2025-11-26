using b._EcommerceWebApp.Sevices.Authentication;
using EcommerceWebApp.DTO_s.Authentication;
using EcommerceWebApp.DTO_s.UserModels;
using ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceWebApp.WebApi.Controllers.Authentication
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _authService = authService;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody] LoginRequestModel request)
        {
            _logger.LogInformation("Fetching User by Email {Email}", request.Email);

            var user = await _authService.GetUserByEmailAsync(request.Email, request.Password);

            if (user == null)
            {
                _logger.LogWarning("No User found with this credentials...");
                return Unauthorized("Invalid User Credentials..");
            }

            _logger.LogInformation("Generating token for User: {Email}", user.Email);
            var token = IssueToken(user.ToModel());

            _logger.LogInformation("Successfully issued token to {Email}", user.Email);
            return Ok(new { Token = token, Payload = user.ToSafeModel() });
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] UserModel request)
        {

            _logger.LogInformation("Adding a new Customer User {user} to Database.", request.Email);

            var user = await _authService.AddUserAsync(request.ToEntity(), 5);

            _logger.LogInformation("User: {Email} added to database as a Guest Customer", user.Email);


            _logger.LogInformation("Generating token for User: {Email}", user.Email);
            var token = IssueToken(user.ToModel());

            _logger.LogInformation("Successfully issued token to {Email}", user.Email);
            return Ok(new { Token = token, Payload = user.ToSafeModel() });
        }

        private string IssueToken(UserModel user)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new List<Claim>
            {
                new Claim("Myapp_User_Id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            _logger.LogInformation("Assembling Token..");
            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );


            _logger.LogInformation("Issuing...");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
