
using motorak.dal.Entites;
using Motorak.DAL.Entites;

namespace Motorak.DAL.Repo.Abstractions
{
    public interface IMechanicRebo
    {
        Task<List<Mechanic>> GetAllAsync();
        Task<Mechanic?> GetByIdAsync(int id);
        Task AddAsync(Mechanic mechanic);
        void Update(Mechanic mechanic);
        void Delete(Mechanic mechanic);
        Task<int> SaveChangesAsync();
    }
}
