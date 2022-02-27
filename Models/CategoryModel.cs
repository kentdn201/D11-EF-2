using System.ComponentModel.DataAnnotations;
using D11.Data.Entites;

namespace D11.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public IEnumerable<ProductViewModel>? Products { get; set; }
    }

    public class CategoryCreateModel
    {
        [Required, MaxLength(35)]
        public string? Name { get; set; }
        public IEnumerable<ProductCreateModel>? Products { get; set; }
    }

    public class CategoryUpdateModel
    {
        [Required, MaxLength(35)]
        public string? Name { get; set; }
    }
}