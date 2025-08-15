using AutoMapper;
using motorak.dal.Entites;
using Motorak.BLL.ModelVM.Customer;
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
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.UserId, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.WorkHours, opt => opt.MapFrom(src => src.WorkHours))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => Enum.Parse<MechanicStatus>(src.Status)))
                .ForMember(m => m.Rating, opt => opt.MapFrom(src => 0)) // Default rating
                .ForMember(m => m.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(m => m.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(m => m.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
                .ForMember(m => m.UpdatedAt, opt => opt.Ignore())
                .ForMember(m => m.IsUpdated, opt => opt.Ignore())
                .ForMember(m => m.Services, opt => opt.Ignore());

            CreateMap<CreateMechanicModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore()) // Mechanics might not have phone in create model
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Password will be handled by Identity
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore());

            CreateMap<EditMechanicModel, Mechanic>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.WorkHours, opt => opt.MapFrom(src => src.WorkHours))
                .ForMember(m => m.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(m => m.UserId, opt => opt.Ignore())
                .ForMember(m => m.User, opt => opt.Ignore())
                .ForMember(m => m.CreatedAt, opt => opt.Ignore())
                .ForMember(m => m.IsDeleted, opt => opt.Ignore())
                .ForMember(m => m.DeletedAt, opt => opt.Ignore())
                .ForMember(m => m.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(m => m.IsUpdated, opt => opt.MapFrom(src => src.IsUpdated))
                .ForMember(m => m.Services, opt => opt.Ignore());

            CreateMap<EditMechanicModel, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.Ignore()) // Don't change username
                .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore()) // Don't change normalized email
                .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore()) // Don't change normalized username
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore());

            CreateMap<User, EditMechanicModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.WorkHours, opt => opt.Ignore())
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsUpdated, opt => opt.Ignore());
        }
    }
}