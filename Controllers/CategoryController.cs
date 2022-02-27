using D11.Models;
using D11.Services;
using Microsoft.AspNetCore.Mvc;

namespace D11.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryService _categoryService;

    public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await _categoryService.GetAllAsync();
        var results = from item in entities
                      select new CategoryViewModel
                      {
                          Id = item.Id,
                          Name = item.Name,
                          Products = from p in item.Products
                                     select new ProductViewModel
                                     {
                                         Id = p.Id,
                                         Name = p.Name,
                                         Manufacture = p.Manufacture
                                     }
                      };
        return new JsonResult(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneAsync(int id)
    {
        var entity = await _categoryService.GetOneAsync(id);
        if (entity == null) return NotFound();

        var result = new JsonResult(new CategoryViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Products = from p in entity.Products
                       select new ProductViewModel
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Manufacture = p.Manufacture
                       }
        });
        return result;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateModel model)
    {
        try
        {
            var entity = new Data.Entites.Category
            {
                Name = model.Name,
                Products = (from p in model.Products
                            select new Data.Entites.Product
                            {
                                Name = p.Name,
                                Manufacture = p.Manufacture
                            }).ToList()
            };

            var result = await _categoryService.AddAsync(entity);
            return new JsonResult(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, CategoryUpdateModel model)
    {
        try
        {
            var entity = await _categoryService.GetOneAsync(id);
            if (entity == null) return NotFound();

            entity.Name = model.Name;

            var edit = await _categoryService.EditAsync(id, entity);
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
            var entity = await _categoryService.GetOneAsync(id);
            if (entity == null) return NotFound();

            await _categoryService.RemoveAsync(id);
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