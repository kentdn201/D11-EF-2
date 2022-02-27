using D11.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace D11.Data.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetAllIncludedAsync();
    Task<IEnumerable<Category>> GetOneIncludedAsync();
}

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(MyDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetAllIncludedAsync()
    {
        return await _entities.Include(c => c.Products).ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetOneIncludedAsync()
    {
        return await _entities.Include(c => c.Products).ToListAsync();
    }
}