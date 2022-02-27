using System.Linq;
using D11.Data;
using D11.Data.Entites;
using D11.Data.Repositories;

namespace D11.Services;

public interface ICategoryService
{
    public Task<IList<Category>?> GetAllAsync();

    public Task<Category?> GetOneAsync(int id);

    public Task<Category?> AddAsync(Category entity);

    public Task<Category?> EditAsync(int id, Category entity);

    public Task RemoveAsync(int id);
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IList<Category>?> GetAllAsync()
    {
        var data = await _categoryRepository.GetAllIncludedAsync();
        return data.ToList();
    }

    public async Task<Category?> GetOneAsync(int id)
    {
        var products = await _categoryRepository.GetOneIncludedAsync();
        return await _categoryRepository.GetAsync(id);
    }

    public async Task<Category?> AddAsync(Category entity)
    {
        await _categoryRepository.InsertAsync(entity);
        return entity;
    }

    public async Task<Category?> EditAsync(int id, Category entity)
    {
        await _categoryRepository.UpdateAsync(entity);
        return entity;
    }


    public async Task RemoveAsync(int id)
    {
        var entity = await _categoryRepository.GetAsync(id);
        if (entity == null) return;

        await _categoryRepository.DeleteAsync(entity);
    }
}