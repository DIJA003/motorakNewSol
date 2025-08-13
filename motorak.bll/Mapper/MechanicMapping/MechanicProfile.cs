
using AutoMapper;
using motorak.dal.Entites;
using Motorak.BLL.ModelVM.Mechanic;
using Motorak.DAL.Entites;
using Motorak.DAL.Enums.MechaincEnums;

namespace Motorak.BLL.Mapper.MechanicMappin
{
    public class MechanicProfile : Profile
    {
        public MechanicProfile()
        {
            
            CreateMap<Mechanic, MechanicListModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(m => m.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(m => m.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(m => m.IsUpdated, opt => opt.MapFrom(src => src.IsUpdated))
                .ForMember(m => m.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(m => m.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(m => m.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt));




            CreateMap<Mechanic, MechanicDetailsModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(m => m.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(m => m.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(m => m.WorkHours, opt => opt.MapFrom(src => src.WorkHours))
                .ForMember(m => m.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(m => m.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(m => m.IsUpdated, opt => opt.MapFrom(src => src.IsUpdated))
                .ForMember(m => m.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(m => m.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(m => m.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt));



            CreateMap<CreateMechanicModel, Mechanic>()
                .ForMember(m => m.WorkHours, opt => opt.MapFrom(src => src.WorkHours))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => Enum.Parse<MechanicStatus>(src.Status)))
                .ForMember(m => m.Rating, opt => opt.MapFrom(src => 0)) 
                .ForMember(m => m.UserId, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore());

            CreateMap<EditMechanicModel, Mechanic>()
                .ForMember(m => m.WorkHours, opt => opt.MapFrom(src => src.WorkHours))
                .ForMember(m => m.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => src.Status))
                .ForAllMembers(opt => opt.Ignore());

            CreateMap<EditMechanicModel, User>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllMembers(opt => opt.Ignore());
        }
    }
}
