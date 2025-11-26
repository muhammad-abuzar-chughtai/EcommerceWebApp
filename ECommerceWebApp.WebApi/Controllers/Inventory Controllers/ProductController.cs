using EcommerceWebApp.DTO_s.InventoryModels;
using EcommerceWebApp.Sevices.Inventory_Service;
using ECommerceWebApp.WebApi.Middlewares.Inventory_Model_Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECommerceWebApp.WebApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<IProductService> _logger;

        public ProductController(IProductService productService, ILogger<IProductService> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all Products.");

            var response = (await _productService.GetAllAsync()).ToModel();

            if (response == null)
            {
                _logger.LogWarning("No Product found in Database.");
                return NotFound(new { message = "No Product was found." } );
            }

            _logger.LogInformation("Successfully retrived {Count} Products", response.Count);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            _logger.LogInformation("Fetching Product by Id: \"{id}\"", id);

            var response = (await _productService.GetbyIdAsync(id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("No Product was found by this Id: \"{id}\"", id);
                return NotFound(new { message = $"No Product was found by this id {id}" });
            }

            _logger.LogInformation("Successfully retrive product: {name} with id: {id}", response.Name, response.Id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Create([FromBody] ProductModel request, int id)
        {
            _logger.LogInformation("Creating product: {Name}", request.Name);

            var response = (await _productService.CreateAsync(request.ToEntity(),id)).ToModel();

            _logger.LogInformation("Successfully Created a Product {Name}", response.Name);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] ProductModel request, int id)
        {
            _logger.LogInformation("Fetching the Product ({id}) to Update.", id);

            var response = (await _productService.UpdateAsync(request.ToEntity(), id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the Product ({id}).", id);
                return NotFound(new { message = $"No Product was found with Id: {id}" });
            }

            _logger.LogInformation("Successfully Updated Product with Id: {id}", response.Id);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Fetching the Product ({id}) to Delete.", id);

            var response = (await _productService.DeleteAsync(id)).ToModel();

            if (response == null)
            {
                _logger.LogWarning("Failed to Fetch the Product ({id}).", id);
                return NotFound(new { message = $"No Product was found with Id: {id}" });
            }

            _logger.LogInformation("Successfully Deleted Product with Id: {id}", response.Id);
            return Ok(response);
        }


    }
}
