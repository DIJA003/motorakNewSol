

using Motorak.BLL.ModelVM.ServiceReview;

namespace Motorak.BLL.Services.Abstractions
{
    public interface IServiceReviewService
    {
        Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetAllAsync();
        Task<(bool status, string message, ServiceReviewDTO? review)> GetByIdAsync(int id);
        Task<(bool status, string message)> CreateAsync(CreateServiceReviewVM model);
        Task<(bool status, string message)> UpdateAsync(UpdateServiceReviewVM model);
        Task<(bool status, string message)> DeleteAsync(int id);

        Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetByServiceIdAsync(int serviceId);
        Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetByCustomerIdAsync(int customerId);
        Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetByRatingAsync(int rating);
    }
}
