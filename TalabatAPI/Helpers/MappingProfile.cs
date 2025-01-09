
using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Order_Aggregate;
using TalabatAPI.DTO;

namespace TalabatAPI.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(D => D.Brand,o=>o.MapFrom(S=>S.Brand.Name))
                .ForMember(D => D.Category, o => o.MapFrom(S => S.Category.Name))
                .ForMember(D=>D.PictureUrl,o=>o.MapFrom<PictureUrlProductMappingProfile>());

            CreateMap<CustomerBasketDTO, CustomerBasket>();

            CreateMap<BasketItemDTO, BasketItem>();

            CreateMap<AddressDTO, Talabat.Core.Entities.Identity.Address>().ReverseMap();
            CreateMap<AddressDTO, Talabat.Core.Order_Aggregate.Address>().ReverseMap();
           

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(D => D.deliveryMethod, S => S.MapFrom(S => S.deliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, S => S.MapFrom(S => S.deliveryMethod.Cost))
                
                ;

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(D => D.ProductId, S => S.MapFrom(S => S.productItem.ProductId))
               .ForMember(D => D.ProductName, S => S.MapFrom(S => S.productItem.ProductName))
               .ForMember(D => D.ProductURL, S => S.MapFrom<PictureUrlOrderMappingProfile>());
               
               ;

           
        }

    }
}
