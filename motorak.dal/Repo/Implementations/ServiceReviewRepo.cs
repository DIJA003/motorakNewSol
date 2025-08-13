

using Microsoft.EntityFrameworkCore;
using motorak.DAL.DataBase;
using Motorak.DAL.Entites;
using Motorak.DAL.Repo.Abstractions;

namespace Motorak.DAL.Repo.Implementations
{
    public class ServiceReviewRepo:IServiceReviewRepo
    {
        private readonly MotorakDbContext db;
        public ServiceReviewRepo(MotorakDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(ServiceReview review)
        {
            await db.ServiceReviews.AddAsync(review);
            await db.SaveChangesAsync();
        }

        public async Task Delete(ServiceReview review)
        {
            review.Delete();
            db.ServiceReviews.Update(review);
            await db.SaveChangesAsync();
        }

        public async Task<List<ServiceReview>> GetAllAsync()
        {
            return await db.ServiceReviews
                   .Where(r => !r.IsDeleted)
                   .ToListAsync();
        }

        public async Task<List<ServiceReview>> GetByCustomerIdAsync(int customerId)
        {
            return await db.ServiceReviews
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<ServiceReview?> GetByIdAsync(int id)
        {
            return await db.ServiceReviews
                    .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<List<ServiceReview>> GetByRatingAsync(int rating)
        {
            return await db.ServiceReviews
                .Where(r => r.Rating == rating)
                .ToListAsync();
        }

        public async Task<List<ServiceReview>> GetByServiceIdAsync(int serviceId)
        {
            return await db.ServiceReviews
                .Where(r => r.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task Update(ServiceReview review)
        {
            db.Update(review);
            await db.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
