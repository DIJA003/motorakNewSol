
using AutoMapper;
using motorak.dal.Entites;
using Motorak.BLL.ModelVM.Car;
using Motorak.DAL.Entites;

namespace Motorak.BLL.Mapper.CarMapping
{
    public class CarProfile : Profile
    {
        public CarProfile() 
        {
            CreateMap<Car, CarListModel>();
            CreateMap<Car, CarDetailsModel>()
                .ForMember(m => m.OwnerName, o => o.MapFrom(c => c.Customer.User.Name));

            CreateMap<CreateCarModel, Car>().ConstructUsing(c => new Car(c.Brand,
                c.Model,
                c.Year,
                c.Color,
                c.Mileage,
                c.Type,
                c.Transmission,
                c.Condition,
                c.Price,
                null));
            
            CreateMap<EditCarModel,Car>().ReverseMap();
        }
    }
}
