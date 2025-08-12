

using Microsoft.EntityFrameworkCore;
using motorak.DAL.DataBase;
using Motorak.DAL.Entites;
//using Motorak.DAL.Enums.SeviceEnums;
using Motorak.DAL.Enums.SeviceEnums;
using Motorak.DAL.Repo.Abstractions;
using System.Security.Cryptography;

namespace Motorak.DAL.Repo.Implementations
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly MotorakDbContext db;
        public ServiceRepo(MotorakDbContext db)
        {
            this.db = db;
        }
        public IQueryable<Service> GetAllQueryable()
        {
            return db.Services.AsQueryable();
        }

        public async Task CreateAsync(Service service)
        {
            await db.Services.AddAsync(service);
        }
        public void Delete(Service service)
        {
            service.Delete();
            db.Services.Update(service);
        }
        public async Task<List<Service>> GetAllAsync()
        {
            return await db.Services.ToListAsync();
        }

        public async Task<List<Service>> GetByCarIdAsync(int carId)
        {
            return await db.Services
                .Where(s => s.CarId == carId)
                .ToListAsync();
        }

        public async Task<List<Service>> GetByCustomerIdAsync(int customerId)
        {
            return await db.Services
                .Where(s => s.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Service>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await db.Services
                .Where(s => s.RequestDate >= from && s.RequestDate <= to)
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await db.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Service>> GetByMechanicIdAsync(int mechanicId)
        {
            return await db.Services
                .Where(s => s.MechanicId == mechanicId)
                .ToListAsync();
        }

        public async Task<List<Service>> GetByStatusAsync(Status status)
        {
            return await db.Services
                .Where(s => s.Status == status)
                .ToListAsync();
        }
        public Task<int> SaveChangesAsync()
        {
            return db.SaveChangesAsync();
        }

        public void Update(Service service)
        {
            db.Services.Update(service);
        }
    }
}
