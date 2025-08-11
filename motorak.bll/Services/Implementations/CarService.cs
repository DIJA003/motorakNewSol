
using AutoMapper;
using motorak.dal.Entites;
using motorak.dal.Entities;
using Motorak.BLL.ModelVM.Car;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAl.Repo.Abstractions;
using Motorak.DAL.Entites;
using Motorak.DAL.Entities;
using Motorak.DAL.Enums.CarEnums;
using Motorak.DAL.Repo.Abstractions;

namespace Motorak.BLL.Services.Implementations
{
    public class CarService : ICarServicecs
    {
        private readonly ICarRebo _carRebo;
        private readonly ICustomerRebo _customerRebo;
        private readonly IPurchaseRepo _purchaseRepo;
        private readonly IRentRepo _rentRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IMapper _mapper;

        public CarService(ICarRebo carRebo, ICustomerRebo customerRebo, IMapper mapper, IPurchaseRepo purchaseRepo, IRentRepo rentRepo, ITransactionRepo transactionRepo)
        {
            _carRebo = carRebo;
            _customerRebo = customerRebo;
            _mapper = mapper;
            _purchaseRepo = purchaseRepo;
            _rentRepo = rentRepo;
            _transactionRepo = transactionRepo;

        }

        public async Task<(bool status, string messagee)> CreateCarAsync(CreateCarModel car)
        {
            try
            {
                if (car == null) return (false, "Car data is null");
                var result = _mapper.Map<Car>(car);
                await _carRebo.CreateAsync(result);
                return (true, "Car Created Successfully");
            }
            catch (Exception ex) 
            {
                return (false, $"Error Ocurred: {ex.Message}");
            }
        }

        public async Task<(bool status, string message)> DeleteCarAsync(int id)
        {
            try
            {
                var result = await _carRebo.GetByIdAsync(id);
                if (result == null) return (false, "Car not Found");
                _carRebo.Delete(result);
                await _carRebo.SaveChangesAsync();
                return (true, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to delete due to : {ex.Message}");
            }
        }

        public async Task<(bool status, string message, List<CarListModel> Cars)> GetAllCarsAsync()
        {
            try
            {
                var result  = await _carRebo.GetAllAsync();
                var resultList = _mapper.Map<List<CarListModel>>(result);
                return (true, "Car retrieved successfully", resultList);
            }
            catch (Exception ex) 
            {
                return (false, $"Failed to retrieve cars : {ex.Message}", new List<CarListModel>());
            }
        }

        public async Task<(bool status, string message, CarDetailsModel? Car)> GetCarByIdAsync(int id)
        {
            try
            {
                var car = await _carRebo.GetByIdAsync(id);
                if (car == null) return (false, "Car not Found", null);
                var result = _mapper.Map<CarDetailsModel>(car);
                return (true, "Car retrieved successfully", result);
            }
            catch (Exception ex)
            {
                return (false, $"Failed to retrieve car : {ex.Message}", null);
            }
        }


        public async Task<(bool status, string message)> RentCarAsync(int carId, int customerId)
        {
            try
            {
                var car = await _carRebo.GetByIdAsync(carId);
                if (car == null)
                    return (false, "Car not found");

                if (car.Status != CarStatus.Available)
                    return (false, "Car is not available");

                car.RentToCustomer(customerId);
                _carRebo.Update(car);

                var rent = new Rent(
                    startDate: DateTime.Now,
                    endDate: DateTime.Now.AddDays(10),
                    paymentMethod: "Online",
                    totalPrice: car.Price,
                    customerId: customerId,
                    carId: carId
                );
                await _rentRepo.AddAsync(rent);

                var trans = new Transactions(
                    paymentMethod: "Online",
                    totalPrice: car.Price,
                    customerId: customerId,
                    carId: carId
                );
                await _transactionRepo.AddAsync(trans);

                await _carRebo.SaveChangesAsync();
                return (true, "Car rented successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to rent car: {ex.Message}");
            }
        }

        public async Task<(bool status, string message)> SellCarAsync(int carId, int customerId)
        {
            try
            {
                var car = await _carRebo.GetByIdAsync(carId);
                if (car == null)
                    return (false, "Car not found");

                if (car.Status != CarStatus.Available)
                    return (false, "Car is not available");

                car.SellToCustomer(customerId);
                _carRebo.Update(car);

                var purchase = new Purchase(
                    sellerName: "Motorak Admin",
                    paymentMethod: "Cash",
                    totalPrice: car.Price,
                    customerId: customerId,
                    carId: carId
                );
                await _purchaseRepo.AddAsync(purchase);

                var trans = new Transactions(
                    paymentMethod: "Cash",
                    totalPrice: (int)car.Price,
                    customerId: customerId,
                    carId: carId
                );
                await _transactionRepo.AddAsync(trans);

                await _carRebo.SaveChangesAsync();
                return (true, "Car sold successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to sell car: {ex.Message}");
            }
        }
        public async Task<(bool status, string message)> UpdateCarAsync(EditCarModel car)
        {
            try
            {
                var result = await _carRebo.GetByIdAsync(car.Id);
                if (result == null) return (false, "Car not found");
                _mapper.Map(car, result);
                _carRebo.Update(result);
                await _carRebo.SaveChangesAsync();
                return (true, "Car Updated Successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Failed to update car: {ex.Message}");
            }
        }
    }
}
