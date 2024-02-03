using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be grater than zero!!")]
        public int Quantity { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be one item at least!!")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
    }
}