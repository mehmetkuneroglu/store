using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ProductDto
    {
        public int ProductId { get; init; }

        [Required]
        [DisplayName("Ürün Adı")]
        public string? ProductName { get; init; }

        [Required]
        [DisplayName("Fiyat")]
        public decimal Price { get; init; }
        public string? Summary { get; init; } = string.Empty;        
        public string? ImageUrl { get; set; }
        public string? CurrentImageUrl { get; set; } = string.Empty;
        public int? CategoryId { get; init; }   // Foreign Key
    }
}