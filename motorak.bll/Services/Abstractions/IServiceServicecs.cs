
using Motorak.BLL.ModelVM.Service;
using Motorak.DAL.Entites;
using Motorak.DAL.Enums.SeviceEnums;

namespace Motorak.BLL.Services.Abstractions
{
    public interface IServiceServicecs
    {
        Task<(bool status, string message, List<ServiceDTO> services)> GetAllServicesAsync();
        Task<(bool status, string message, ServiceDTO? service)> GetServiceByIdAsync(int id);
        Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByCustomerIdAsync(int customerId);
        Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByMechanicIdAsync(int mechanicId);
        Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByStatusAsync(Status status);
        Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByDateRangeAsync(DateTime from, DateTime to);
        Task<(bool status, string message, List<ServiceDTO> services)> GetServicesByCarIdAsync(int carId);

        Task<(bool status, string message)> CreateServiceAsync(CreateServiceVM service);
        Task<(bool status, string message)> UpdateServiceStatusAsync(UpdateServiceVM upService);
        Task<(bool status, string message)> DeleteServiceAsync(int serviceId);
    }
}
