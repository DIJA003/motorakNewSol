using Motorak.DAL.Entities;


namespace Motorak.DAL.Repo.Abstractions
{
    public interface IRentRepo
    {
        Task<Rent?> GetByIdAsync(int id);
        Task<List<Rent>> GetAllAsync();
        Task AddAsync(Rent rent);
        Task UpdateAsync(Rent rent);
        Task DeleteAsync(int id);
    }
}
