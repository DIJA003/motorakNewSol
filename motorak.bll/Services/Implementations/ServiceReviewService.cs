
using AutoMapper;
using Motorak.BLL.ModelVM.ServiceReview;
using Motorak.BLL.Services.Abstractions;
using Motorak.DAL.Entites;
using Motorak.DAL.Repo.Abstractions;

namespace Motorak.BLL.Services.Implementations
{
    public class ServiceReviewService : IServiceReviewService
    {
        private readonly IServiceReviewRepo _reviewRepo;
        public readonly IMapper _mapper;
        public ServiceReviewService(IServiceReviewRepo reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task<(bool status, string message)> CreateAsync(CreateServiceReviewVM model)
        {
            try
            {
                var result = _mapper.Map<ServiceReview>(model);
                await _reviewRepo.CreateAsync(result);
                return (true, "Service review created successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool status, string message)> DeleteAsync(int id)
        {
            try
            {
                var result = await _reviewRepo.GetByIdAsync(id);
                if (result == null)
                {
                    return (false, "Service review not found.");
                }

                await _reviewRepo.Delete(result);

                return (true, "Service review deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetAllAsync()
        {
            try
            {
                var reviews = await _reviewRepo.GetAllAsync();
                var reviewDTOs = _mapper.Map<List<ServiceReviewDTO>>(reviews);
                return (true, "Service reviews retrieved successfully.", reviewDTOs);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new List<ServiceReviewDTO>());
            }

        }

        public async Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetByCustomerIdAsync(int customerId)
        {
            try
            {
                var reviews = await _reviewRepo.GetByCustomerIdAsync(customerId);
                var reviewDTOs = _mapper.Map<List<ServiceReviewDTO>>(reviews);
                return (true, "Service reviews retrieved successfully.", reviewDTOs);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new List<ServiceReviewDTO>());

            }
        }

        public async Task<(bool status, string message, ServiceReviewDTO? review)> GetByIdAsync(int id)
        {
            try
            {
                var review = await _reviewRepo.GetByIdAsync(id);
                if (review == null)
                {
                    return (false, "Service review not found.", null);
                }
                var reviewDTO = _mapper.Map<ServiceReviewDTO>(review);
                return (true, "Service review retrieved successfully.", reviewDTO);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);

            }
        }

        public async Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetByRatingAsync(int rating)
        {
            try
            {
                var reviews = await _reviewRepo.GetByRatingAsync(rating);
                var reviewDTOs = _mapper.Map<List<ServiceReviewDTO>>(reviews);
                return (true, "Service reviews retrieved successfully.", reviewDTOs);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new List<ServiceReviewDTO>());
            }
        }

        public async Task<(bool status, string message, List<ServiceReviewDTO> reviews)> GetByServiceIdAsync(int serviceId)
        {
            try
            {
                var reviews = await _reviewRepo.GetByServiceIdAsync(serviceId);
                var reviewDTOs = _mapper.Map<List<ServiceReviewDTO>>(reviews);
                return (true, "Service reviews retrieved successfully.", reviewDTOs);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, new List<ServiceReviewDTO>());
            }
        }

        public async Task<(bool status, string message)> UpdateAsync(UpdateServiceReviewVM model)
        {
            try
            {
                var existingReview = await _reviewRepo.GetByIdAsync(model.ReviewId);
                if (existingReview == null)
                    return (false, "Service review not found.");

                _mapper.Map(model, existingReview);

                await _reviewRepo.Update(existingReview);

                return (true, "Service review updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    }
}
