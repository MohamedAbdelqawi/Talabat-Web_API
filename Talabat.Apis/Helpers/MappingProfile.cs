using AutoMapper;
using Talabat.Apis.Dtos;
using Talabat.Apis.Extentions;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>().ForMember(x => x.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                                                   .ForMember(x => x.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                                                   .ForMember(x => x.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Talabat.Core.Entities.Order_Aggregate.Address>();
        }
    }
}
