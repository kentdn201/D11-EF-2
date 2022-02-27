using System.ComponentModel.DataAnnotations;
using D11.Data.Entities;

namespace D11.Data.Entites;

public class Category : BaseEntity
{
    [Required, MaxLength(35)]
    public string? Name { get; set; }

    public ICollection<Product>? Products { get; set; }
}
