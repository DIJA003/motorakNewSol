

using Motorak.DAL.Entites;

namespace Motorak.DAL.Repo.Abstractions
{
    public interface IServiceReviewRepo
    {
        Task<List<ServiceReview>> GetAllAsync();
        Task<ServiceReview?> GetByIdAsync(int id);
        Task<List<ServiceReview>> GetByServiceIdAsync(int serviceId);
        Task<List<ServiceReview>> GetByCustomerIdAsync(int customerId);
        Task<List<ServiceReview>> GetByRatingAsync(int rating);

        Task CreateAsync(ServiceReview review);
        void Update(ServiceReview review);
        void Delete(ServiceReview review);
    }
}
