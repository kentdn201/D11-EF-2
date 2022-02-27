using D11.Models;
using D11.Services;
using Microsoft.AspNetCore.Mvc;

namespace D11.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await _productService.GetAllAsync();
        var results = from p in entities
                      select new ProductViewAllModel
                      {
                          Id = p.Id,
                          Name = p.Name,
                          Manufacture = p.Manufacture,
                          CategoryId = p.CategoryId
                      };
        return new JsonResult(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneAsync(int id)
    {
        var entity = await _productService.GetOneAsync(id);
        if (entity == null) return NotFound();

        return new JsonResult(new ProductViewAllModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Manufacture = entity.Manufacture,
            CategoryId = entity.CategoryId
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(ProductCreateModel model)
    {
        try
        {
            var entity = new Data.Entites.Product
            {
                Name = model.Name,
                Manufacture = model.Manufacture,
                CategoryId = model.CategoryId
            };

            var result = await _productService.AddAsync(entity);
            return new JsonResult(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, ProductUpdateModel model)
    {
        try
        {
            var entity = await _productService.GetOneAsync(id);
            if (entity == null) return NotFound();

            entity.Name = model.Name;
            entity.Manufacture = model.Manufacture;
            entity.CategoryId = model.CategoryId;

            var edit = await _productService.EditAsync(id, entity);
            return new JsonResult(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var entity =  await _productService.GetOneAsync(id);
            if (entity == null) return NotFound();

            await _productService.RemoveAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        throw new NotImplementedException();
    }
}