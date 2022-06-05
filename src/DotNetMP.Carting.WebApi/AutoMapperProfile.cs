using AutoMapper;
using DotNetMP.Carting.Core.Aggregates.CartAggregate;
using DotNetMP.Carting.WebApi.Endpoints.AddItemToCartEndpoint;
using DotNetMP.Carting.WebApi.Endpoints.RemoveItemFromCartEndpoint;
using DotNetMP.Carting.WebApi.ViewModels;

namespace DotNetMP.Carting.WebApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CartViewModel, Cart>()
            .DisableCtorValidation()
            .ReverseMap();
        CreateMap<ItemViewModel, Item>()
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("price", opt => opt.MapFrom(src => src.Price))
            .ForCtorParam("quantity", opt => opt.MapFrom(src => src.Quantity))
            .ForCtorParam("image", opt => opt.MapFrom(src => src.Image))
            .DisableCtorValidation()
            .ReverseMap().DisableCtorValidation();
        CreateMap<ImageViewModel, Image>()
            .DisableCtorValidation()
            .ReverseMap();

        CreateMap<Cart, AddItemToCartResponse>();
        CreateMap<Cart, RemoveItemFromCartResponse>();
    }
}
