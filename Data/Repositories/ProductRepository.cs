using D11.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace D11.Data.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    // Task<IEnumerable<Product>> GetAllIncludedAsync();
    // Task<Product?> GetOneCategoryAsync(int id);
}

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context)
    {
    }

    // public async Task<IEnumerable<Product>> GetAllIncludedAsync()
    // {
    //    throw new NotImplementedException();
    // }

    // public async Task<Product?> GetOneCategoryAsync(int id)
    // {
    //     return await _entities.SingleOrDefaultAsync(category => category.Id == id);
    // }
}