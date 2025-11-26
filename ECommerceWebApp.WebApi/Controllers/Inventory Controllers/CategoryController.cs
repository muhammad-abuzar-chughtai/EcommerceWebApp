using EcommerceWebApp.DTO_s.InventoryModels;
using EcommerceWebApp.Sevices.Inventory_Service;
using ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApp.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ICategoryService> _logger;
        public CategoryController(ICategoryService categoryService, ILogger<ICategoryService> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all Categories.");

            var response = (await _categoryService.GetAllAsync()).ToModel();

            if (response.Count() <= 0)
            {
                _logger.LogWarning("No Category Found in Database.");
                return NotFound(new { message = "No Categories Found." });
            }

            _logger.LogInformation("Successfully retrived {Count} Categories", response.Count);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching Category by Id: \"{id}\"", id);

            var response = (await _categoryService.GetbyIdAsync(id)).ToModel();
            if (response == null)
            {
                _logger.LogWarning("No Category was found by this Id: \"{id}\"", id);
                return NotFound(new { message = $"No Category was found by this id {id}" });
            }

            _logger.LogInformation("Successfully retrive category: {name} with id: {id}", response.Name, response.Id);
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CategoryModel request)
        {
            _logger.LogInformation("Creating category: {Name}", request.Name);

            var response = (await _categoryService.CreateAsync(request.ToEntity())).ToModel();

            _logger.LogInformation("Successfully Created a Category {Name}", response.Name);
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] CategoryModel request, int id)
        {
            _logger.LogInformation("Fetching the Category ({id}) to Update.", id);

            var response = (await _categoryService.UpdateAsync(request.ToEntity(), id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the Category ({id}).", id);
                return NotFound(new { message = $"No Category was found with Id: {id}"} );
            }

            _logger.LogInformation("Successfully Updated Category with Id: {id}", response.Id);
            return Ok(response);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Fetching the Category ({id}) to Delete.", id);

            var response = (await _categoryService.DeleteAsync(id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the Category ({id}).", id);
                return NotFound(new { message = $"No Category was found with Id: {id}" });
            }
            _logger.LogInformation("Successfully Deleted Category with Id: {id}", response.Id);
            return Ok(response);
        }
    }
}
