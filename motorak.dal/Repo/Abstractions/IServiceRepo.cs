

using Motorak.DAL.Entites;
//using Motorak.DAL.Enums.ServiceReviewEnums;
using Motorak.DAL.Enums.SeviceEnums;

namespace Motorak.DAL.Repo.Abstractions
{
    public interface IServiceRepo
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<List<Service>> GetByCustomerIdAsync(int customerId);
        Task<List<Service>> GetByMechanicIdAsync(int mechanicId);
        Task<List<Service>> GetByStatusAsync(Status status);
        Task<List<Service>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<List<Service>> GetByCarIdAsync(int carId);

        Task CreateAsync(Service service);
        Task Update(Service service);
        Task Delete(Service service);
        Task<int> SaveChangesAsync();
    }
}
