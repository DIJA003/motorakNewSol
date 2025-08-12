

using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<(bool status, string message)> CreateServiceAsync(CreateServiceVM service)
        {
            try
            {
                if (service == null) return (false, "Service data is null");
                var result = _mapper.Map<Service>(service);
                await _serviceRebo.CreateAsync(result);
                return (true, "Service Created Successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error Occurred: {ex.Message}");
            }
        }
        public async Task<(bool status, string message, List<ServiceDTO> services)> GetServicesFilteredAsync(
        Status? status, int? carId, int? customerId, DateTime? from, DateTime? to, int? mechanicId)
        {
            try
            {
                var query = _serviceRebo.GetAllQueryable();

                if (status.HasValue)
                    query = query.Where(s => s.Status == status.Value);

                if (carId.HasValue)
                    query = query.Where(s => s.CarId == carId.Value);

                if (customerId.HasValue)
                    query = query.Where(s => s.CustomerId == customerId.Value);

                if (from.HasValue && to.HasValue)
                    query = query.Where(s => s.RequestDate >= from.Value && s.RequestDate <= to.Value);

                if (mechanicId.HasValue)
                    query = query.Where(s => s.MechanicId == mechanicId.Value);

                var list = await query.ToListAsync();
                var dtoList = _mapper.Map<List<ServiceDTO>>(list);

                return (true, "Filtered results", dtoList);
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}", new List<ServiceDTO>());
            }
        }
        public async Task<(bool status, string message)> DeleteServiceAsync(int serviceId)
        {
            try
            {
                var result = await _serviceRebo.GetByIdAsync(serviceId);
                if (result == null) return (false, "Service not Found");
                _serviceRebo.Delete(result);
                await _serviceRebo.SaveChangesAsync();
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
                var result = _serviceRebo.GetByIdAsync(id);
                if (result == null)
                {
                    return (false, "Service not found", null);
                }

                return (true, "Service found", _mapper.Map<ServiceDTO>(await result));
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

        public async Task<(bool status, string message)> UpdateServiceStatusAsync(UpdateServiceVM service)
        {
            try
            {
                var result = await _serviceRebo.GetByIdAsync(service.ServiceId);
                if (result == null) return (false, "Service not found");
                _mapper.Map(service, result);
                _serviceRebo.Update(result);
                await _serviceRebo.SaveChangesAsync();
                return (true, "Service status updated successfully");
            }
            catch (Exception ex)
            {
                return (false, $"Error Occurred: {ex.Message}");
            }
        }
    }
}

