

using AutoMapper;
using Motorak.BLL.ModelVM.Service;
using Motorak.DAL.Entites;

namespace Motorak.BLL.Mapper.ServiceMapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.ServiceType.ToString()));

            CreateMap<CreateServiceVM, Service>();

            CreateMap<UpdateServiceVM, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
