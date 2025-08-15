
using Motorak.BLL.ModelVM.Car;

namespace Motorak.BLL.Services.Abstractions
{
    public interface ICarServicecs
    {
        Task<(bool status, string message, List<CarListModel> Cars)> GetAllCarsAsync();
        Task<(bool status, string message, CarDetailsModel? Car)> GetCarByIdAsync(int id);
        Task<(bool status, string messagee)> CreateCarAsync(CreateCarModel car);
        Task<(bool status, string message)> UpdateCarAsync(EditCarModel car);
        Task<(bool status, string message)> DeleteCarAsync(int id);
        Task<(bool status, string message)> SellCarAsync(int carId, int customerId);
        Task<(bool status, string message)> RentCarAsync(int carId, int customerId);
    }
}