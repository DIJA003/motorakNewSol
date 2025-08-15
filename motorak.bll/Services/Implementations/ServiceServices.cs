

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Motorak.BLL.ModelVM.Service;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAL.Entites;
using Motorak.DAL.Enums.SeviceEnums;
using Motorak.DAL.Repo.Abstractions;
using Motorak.DAL.Repo.Implementations;

namespace Motorak.BLL.Services.Implementations
{
    public class ServiceServices : IServiceServicecs
    {
        private readonly IServiceRepo _serviceRebo;
        private readonly IMapper _mapper;
        public ServiceServices(IServiceRepo serviceRebo, IMapper mapper)
        {
            _serviceRebo = serviceRebo;
            _mapper = mapper;
        }
        //public async Task<(bool status, string message)> CreateServiceAsync(CreateServiceVM service)
        //{
        //    try
        //    {
        //        if (service == null) return (false, "Service data is null");
        //        var result = _mapper.Map<Service>(service);
        //        await _serviceRebo.CreateAsync(result);
        //        return (true, "Service Created Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"Error Occurred: {ex.Message}");
        //    }
        //}
        public async Task<(bool status, string message)> CreateServiceAsync(CreateServiceVM service)
        {
            try
            {
                if (service == null)
                    return (false, "Service data is null");

                var result = _mapper.Map<Service>(service);

                if (result.CarId == 0) return (false, "CarId is required.");
                if (result.MechanicId == 0) return (false, "MechanicId is required.");

                await _serviceRebo.CreateAsync(result);
                return (true, "Service Created Successfully");
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, $"Error Occurred: {innerMessage}");
            }
        }
        public async Task<(bool status, string message)> DeleteServiceAsync(int serviceId)
        {
            try
            {
                var result = await _serviceRebo.GetByIdAsync(serviceId);
                if (result == null) return (false, "Service not Found");
                await _serviceRebo.Delete(result);
                //await _serviceRebo.SaveChangesAsync();
                return (true, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error Occurred: {ex.Message}");
            }
        }

        public async Task<(bool status, string message, List<ServiceDTO> services)> GetAllServicesAsync()
        {
            try
            {
                var result = await _serviceRebo.GetAllAsync();
                var resultLest = _mapper.Map<List<ServiceDTO>>(result);
                return (true, "Fetched", resultLest);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new());
            }
        }

        public async Task<(bool status, string message, ServiceDTO? service)> GetServiceByIdAsync(int id)
        {
            try
            {
                var result =  await _serviceRebo.GetByIdAsync(id);
                if (result == null)
                {
                    return (false, "Service not found", null);
                }

                return (true, "Service found", _mapper.Map<ServiceDTO>(result));
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", null);
            }
        }

        public async Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByCarIdAsync(int carId)
        {
            try
            {
                var result = _serviceRebo.GetByCarIdAsync(carId);
                return (true, "Services found", _mapper.Map<List<ServiceDTO>>(await result));
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new List<ServiceDTO>());
            }
        }

        public async Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByCustomerIdAsync(int customerId)
        {
            try
            {
                var result = await _serviceRebo.GetByCustomerIdAsync(customerId);
                return (true, "Services found", _mapper.Map<List<ServiceDTO>>(result));
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new());
            }
        }

        public async Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByDateRangeAsync(DateTime from, DateTime to)
        {
            try
            {
                var result = _serviceRebo.GetByDateRangeAsync(from, to);
                return (true, "Services found", _mapper.Map<List<ServiceDTO>>(await result));
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new List<ServiceDTO>());
            }
        }

        public async Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByMechanicIdAsync(int mechanicId)
        {
            try
            {
                var result = _serviceRebo.GetByMechanicIdAsync(mechanicId);
                return (true, "Services found", _mapper.Map<List<ServiceDTO>>(await result));
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new List<ServiceDTO>());
            }
        }

        public async Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByStatusAsync(Status status)
        {
            try
            {
                var result = _serviceRebo.GetByStatusAsync(status);
                return (true, "Services found", _mapper.Map<List<ServiceDTO>>(await result));
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new List<ServiceDTO>());
            }
        }

        public async Task<(bool status, string message)> UpdateServiceStatusAsync(UpdateServiceVM model)
        {
            try
            {
                var existingReview = await _serviceRebo.GetByIdAsync(model.ServiceId);
                if (existingReview == null)
                    return (false, "Service review not found.");

                _mapper.Map(model, existingReview);

                await _serviceRebo.Update(existingReview);

                return (true, "Service review updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

