using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motorak.DAL.Entites;
using AutoMapper;

namespace motorak.bll.Mapper.CartMapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, ShoppingCartVM>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Car.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Car.Model))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Car.Year))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Car.Price))
                .ForMember(dest => dest.DailyRentPrice, opt => opt.MapFrom(src => src.Car.DailyRentPrice))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.Car.ImagePath))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Car.Category))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
        }
    }
}
