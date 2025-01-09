using AutoMapper;
using Talabat.Core.Order_Aggregate;
using TalabatAPI.DTO;

namespace TalabatAPI.Helpers
{
    public class PictureUrlOrderMappingProfile : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public PictureUrlOrderMappingProfile(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.productItem.ProductURL))
            {
                return $"{_config["DefualtUrl"]}/{source.productItem.ProductURL}";
            }
            return string.Empty ;
        }
    }
}
