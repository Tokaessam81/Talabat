using System.ComponentModel.DataAnnotations;

namespace TalabatAPI.DTO
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(.1, double.MaxValue, ErrorMessage ="Price must be grater than 0 .")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be grater than 0 .")]
        public int Quantity { get; set; }
    }
}