using EcommerceWebApp.DTO_s.UserModels;
using EcommerceWebApp.Sevices.Inventory_Service;
using EcommerceWebApp.Sevices.User_Service;
using ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<IRoleService> _logger;
        public RoleController(IRoleService roleService, ILogger<IRoleService> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all Roles.");

            var response = (await _roleService.GetAllAsync()).ToModel();

            if (response.Count <= 0)
            {
                _logger.LogWarning("No Role found in Database.");
                return NotFound(new { message = "No Role was found." });
            }

            _logger.LogInformation("Successfully retrived {Count} Roles", response.Count);
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching Role by Id: \"{id}\"", id);

            var response = (await _roleService.GetbyIdAsync(id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("No Role was found by this Id: \"{id}\"", id);
                return NotFound(new { message = $"No Role was found by this id {id}" });
            }

            _logger.LogInformation("Successfully retrive Role: {name} with id: {id}", response.Name, response.Id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] RoleModel request)
        {
            _logger.LogInformation("Creating Role: {Name}", request.Name);

            var response = (await _roleService.CreateAsync(request.ToEntity())).ToModel();

            _logger.LogInformation("Successfully Created a Role {Name}", response.Name);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleModel request, int id)
        {
            _logger.LogInformation("Fetching the Role ({id}) to Update.", id);

            var response = (await _roleService.UpdateAsync(request.ToEntity(), id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the Role ({id}).", id);
                return NotFound(new { message = $"No Role was found with Id: {id}" });
            }

            _logger.LogInformation("Successfully Updated Role with Id: {id}", response.Id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Fetching the Product ({id}) to Delete.", id);

            var response = (await _roleService.DeleteAsync(id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the Role ({id}).", id);
                return NotFound(new { message = $"No Role was found with Id: {id}" });
            }

            _logger.LogInformation("Successfully Deleted Role with Id: {id}", response.Id);
            return Ok(response);
        }
    }
}

