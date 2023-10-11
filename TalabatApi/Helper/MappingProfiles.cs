using AutoMapper;
using CoreLayer.Entities;
using CoreLayer.Entities.Basket_Module;
using CoreLayer.Entities.IdentityModule;
using CoreLayer.Entities.Order_Agregate;
using TalabatApi.Dtos;


namespace TalabatApi.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, O => O.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, O => O.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CoreLayer.Entities.IdentityModule.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, CoreLayer.Entities.Order_Agregate.Address>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}
