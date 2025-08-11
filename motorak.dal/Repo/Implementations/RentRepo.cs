using Microsoft.EntityFrameworkCore;
using motorak.DAL.DataBase;
using Motorak.DAL.Entities;
using Motorak.DAL.Repo.Abstractions;


namespace Motorak.DAL.Repo.Implementations
{
    public class RentRepo : IRentRepo
    {
        private readonly MotorakDbContext _context;

        public RentRepo(MotorakDbContext context)
        {
            _context = context;
        }

        public async Task<Rent?> GetByIdAsync(int id)
        {
            return await _context.Rents.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<List<Rent>> GetAllAsync()
        {
            return await _context.Rents.Where(r => !r.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(Rent rent)
        {
            await _context.Rents.AddAsync(rent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rent rent)
        {
            var existing = await _context.Rents.FirstOrDefaultAsync(r => r.Id == rent.Id && !r.IsDeleted);
            if (existing == null)
                throw new InvalidOperationException("Rent not found");

            existing.UpdateTransaction(rent.PaymentMethod, rent.TotalPrice, rent.Status, rent.UpdatedBy ?? "System");
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rent = await _context.Rents.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
            if (rent == null)
                throw new InvalidOperationException("Rent not found.");

            rent.Delete();
            await _context.SaveChangesAsync();
        }
    }
}

