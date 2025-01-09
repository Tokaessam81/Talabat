using AutoMapper;
using Talabat.Core.Entities;
using TalabatAPI.DTO;

namespace TalabatAPI.Helpers
{
    public class PictureUrlProductMappingProfile : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _config;

        public PictureUrlProductMappingProfile(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_config["DefualtUrl"]}/{source.PictureUrl}";
            }
            return "";
        }
    }
}
