
using AutoMapper;
using motorak.dal.Entites;
using Motorak.BLL.ModelVM.Customer;
using Motorak.DAL.Entites;

namespace Motorak.BLL.Mapper.CustomerMapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerListModel>()
                .ForMember(c => c.Id, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(x => x.User.Name))
                .ForMember(c => c.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(c => c.PurchasedCarsCount, opt => opt.MapFrom(x => x.Purchases.Count))
                .ForMember(c => c.CreatedAt, opt => opt.MapFrom(x => x.User.CreatedAt))
                .ForMember(c => c.IsDeleted, opt => opt.MapFrom(x => x.IsDeleted))
                .ForMember(c => c.DeletedAt, opt => opt.MapFrom(x => x.DeletedAt))
                .ForMember(c => c.IsUpdated, opt => opt.MapFrom(x => x.IsUpdated))
                .ForMember(c => c.UpdatedAt, opt => opt.MapFrom(x => x.UpdatedAt));


            CreateMap<Customer, CustomerDetailsModel>()
                .ForMember(c => c.Id, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(c => c.Name, opt => opt.MapFrom(x => x.User.Name))
                .ForMember(c => c.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(c => c.PurchasedCarsCount, opt => opt.MapFrom(x => x.Purchases.Count))
                .ForMember(c => c.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber))
                .ForMember(c => c.CreatedAt, opt => opt.MapFrom(x => x.User.CreatedAt))
                .ForMember(c => c.IsDeleted, opt => opt.MapFrom(x => x.IsDeleted))
                .ForMember(c => c.DeletedAt, opt => opt.MapFrom(x => x.DeletedAt))
                .ForMember(c => c.IsUpdated, opt => opt.MapFrom(x => x.IsUpdated))
                .ForMember(c => c.UpdatedAt, opt => opt.MapFrom(x => x.UpdatedAt));

            CreateMap<CreateCustomerModel, Customer>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<EditCustomerModel, User>().ReverseMap();
        }
    }
}
