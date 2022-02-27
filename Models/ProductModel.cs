using System.ComponentModel.DataAnnotations;
using D11.Data.Entites;

namespace D11.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Manufacture { get; set; }
    }

    public class ProductViewAllModel : ProductViewModel
    {
        [Required]
        public int CategoryId { get; set; }
    }

    public class ProductCreateModel
    {
        [Required, MaxLength(35)]
        public string? Name { get; set; }
        public string? Manufacture { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class ProductUpdateModel : ProductCreateModel
    {

    }
}