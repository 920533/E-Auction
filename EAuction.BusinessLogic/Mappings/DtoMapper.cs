using EAuction.Shared.Models;
using EAuction.Shared.Seller;
using EAuction.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace EAuction.BusinessLogic.Repository
{
    public class DtoMapper : Profile
    {

        public DtoMapper()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, action => action.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, action => action.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.ShortDeceription, action => action.MapFrom(src => src.ShortDeceription))
                .ForMember(dest => dest.DetailedDeceription, action => action.MapFrom(src => src.DetailedDeceription))
                .ForMember(dest => dest.Category, action => action.MapFrom(src => src.Category))
                .ForMember(dest => dest.BidEndDate, action => action.MapFrom(src => src.BidEndDate))
                .ForMember(dest => dest.StartingPrice, action => action.MapFrom(src => src.StartingPrice));
            CreateMap<ProductToBuyer, ProductToBuyerDto>()
                .ForMember(dest => dest.BuyerProductId, action => action.MapFrom(src => src.BuyerProductId))
                .ForMember(dest => dest.ProductId, action => action.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UserId, action => action.MapFrom(src => src.UserID))
                .ForMember(dest => dest.BidAmount, action => action.MapFrom(src => src.BidAmount));
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, action => action.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, action => action.MapFrom(src => src.UserName))
                .ForMember(dest => dest.FirstName, action => action.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, action => action.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, action => action.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Pin, action => action.MapFrom(src => src.Pin))
                .ForMember(dest => dest.UserType, action => action.MapFrom(src => src.UserType))
                .ForMember(dest => dest.Password, action => action.MapFrom(src => src.Password))
                .ForMember(dest => dest.State, action => action.MapFrom(src => src.State))
                .ForMember(dest => dest.City, action => action.MapFrom(src => src.City))
                .ForMember(dest => dest.Email, action => action.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, action => action.MapFrom(src => src.Address));
        }

    }
}
