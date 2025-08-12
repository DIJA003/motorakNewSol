//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using motorak.dal.Entites;
//using Motorak.BLL.ModelVM.Customer;
//using Motorak.BLL.ModelVM.Mechanic;

//namespace motorak.bll.Mapper.UserMapping
//{
//    public class UserProfile : Profile
//    {
//        public UserProfile()
//        {

//            CreateMap<CreateCustomerModel, User>()
//                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
//                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

//            CreateMap<CreateCustomerModel, Customer>()
//                .ForMember(dest => dest.Id, opt => opt.Ignore())
//                .ForMember(dest => dest.User, opt => opt.Ignore());

//            CreateMap<CreateMechanicModel, User>()
//                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

//            CreateMap<CreateMechanicModel, Mechanic>()
//                .ForMember(dest => dest.Id, opt => opt.Ignore())
//                .ForMember(dest => dest.User, opt => opt.Ignore());
//        }
//    }
//}
