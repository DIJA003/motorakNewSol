

using AutoMapper;
using Motorak.BLL.ModelVM.ServiceReview;
using Motorak.DAL.Entites;

namespace Motorak.BLL.Mapper.ServiceReviewMapping
{
    public class ServiceReviewProfile : Profile
    {
        public ServiceReviewProfile()
        {

            CreateMap<ServiceReview, ServiceReviewDTO>()
            .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.Id)) 
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment));

            CreateMap<CreateServiceReviewVM, ServiceReview>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comments));


            CreateMap<UpdateServiceReviewVM, ServiceReview>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ReviewId))
              .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comments));

        }
    }
}
