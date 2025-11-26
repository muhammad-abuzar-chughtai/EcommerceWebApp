using EcommerceWebApp.DTO_s.UserModels;
using EcommerceWebApp.Sevices.User_Service;
using ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<IRoleService> _logger;


        public UserController(IUserService userService, ILogger<IRoleService> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all Users.");

            var response = (await _userService.GetAllAsync()).ToModel();

            if (response == null)
            {
                _logger.LogWarning("No User found in Database.");
                return NotFound(new { message = "No User was found." });
            }

            _logger.LogInformation("Successfully retrived {Count} Roles", response.Count);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            _logger.LogInformation("Fetching User by Id: \"{id}\"", id);

            var response = (await _userService.GetByIdAsync(id)).ToModel();
            if (response == null)
            {
                _logger.LogWarning("No Users was found by this Id: \"{id}\"", id);
                return NotFound(new { message = $"No Users was found by this id {id}" });
            }

            _logger.LogInformation("Successfully retrive User: {name} with id: {id}", response.Name, response.Id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromBody] UserModel request, int id)
        {
            _logger.LogInformation("Creating User: {Name}", request.Name);

            var response = (await _userService.CreateAsync(request.ToEntity(), id)).ToModel();

            _logger.LogInformation("Successfully Created a User {Name}", response.Name);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UserModel request, int id)
        {
            _logger.LogInformation("Fetching the User ({id}) to Update.", id);

            var response = (await _userService.UpdateAsync(request.ToEntity(), id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the User ({id}).", id);
                return NotFound(new { message = $"No User was found with Id: {id}" });
            }

            _logger.LogInformation("Successfully Updated User with Id: {id}", response.Id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Fetching the User ({id}) to Delete.", id);

            var response = (await _userService.DeleteAsync(id)).ToModel();
            
            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the User ({id}).", id);
                return NotFound(new { message = $"No User was found with Id: {id}" });
            }

            _logger.LogInformation("Successfully Deleted User with Id: {id}", response.Id);
            return Ok(response);
        }
    }
}
