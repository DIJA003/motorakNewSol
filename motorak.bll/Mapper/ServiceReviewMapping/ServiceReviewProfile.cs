

using AutoMapper;
using Motorak.BLL.ModelVM.ServiceReview;
using Motorak.DAL.Entites;

namespace Motorak.BLL.Mapper.ServiceReviewMapping
{
    public class ServiceReviewProfile : Profile
    {
        public ServiceReviewProfile()
        {
            CreateMap<ServiceReview, ServiceReviewDTO>();
            CreateMap<CreateServiceReviewVM, ServiceReview>();
            CreateMap<UpdateServiceReviewVM, ServiceReview>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
