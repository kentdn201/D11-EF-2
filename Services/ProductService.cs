using D11.Data;
using D11.Data.Entites;
using D11.Data.Repositories;

namespace D11.Services;

public interface IProductService
{
    public Task<IList<Product>?> GetAllAsync();

    public Task<Product?> GetOneAsync(int id);

    public Task<Product?> AddAsync(Product entity);

    public Task<Product?> EditAsync(int id, Product entity);

    public Task RemoveAsync(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<Product>?> GetAllAsync()
    {
        var data = await _productRepository.GetAllAsync();
        return data.ToList();
    }

    public async Task<Product?> GetOneAsync(int id)
    {
        var data = await _productRepository.GetAsync(id);
        return data;
    }

    public async Task<Product?> AddAsync(Product entity)
    {
        await _productRepository.InsertAsync(entity);
        return entity;
    }

    public async Task<Product?> EditAsync(int id, Product entity)
    {
        await _productRepository.UpdateAsync(entity);
        return entity;
    }


    public async Task RemoveAsync(int id)
    {
        var entity = await _productRepository.GetAsync(id);
        if (entity == null) return;

        await _productRepository.DeleteAsync(entity);
    }
}