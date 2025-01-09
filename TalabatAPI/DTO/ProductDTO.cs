using Talabat.Core.Entities;

namespace TalabatAPI.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
        //Forign Key
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        //Navigation Property

        public string Brand { get; set; }
        public string Category { get; set; }
    }
}
